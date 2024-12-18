﻿using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NhaHang.Helpers;
using System.IO;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


string keyVaultUri = builder.Configuration["KeyVaultUri"];

builder.Configuration.AddAzureKeyVault(
    new Uri(keyVaultUri),
    new DefaultAzureCredential()
);

var connectionString = builder.Configuration["AzureDBConnectionString"];
// Thêm Db context như một dịch vụ cho ứng dụng
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseSqlServer(connect);
    options.UseSqlServer(connectionString, o =>
    {
        o.EnableRetryOnFailure(
            maxRetryCount:5,
            maxRetryDelay:TimeSpan.FromSeconds(10),
            errorNumbersToAdd:null
            );
    });
});

// Cấu hình JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"], 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                var roleClaim = claimsIdentity?.FindFirst(ClaimTypes.Role);
                if (roleClaim != null)
                {
                    Console.WriteLine("User role: " + roleClaim.Value);  // Kiểm tra role
                }
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

// Cấu hình chính sách ủy quyền
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Management"));
});
// Cấu hình VNPAY settings
builder.Services.Configure<VnpaySettings>(builder.Configuration.GetSection("VnpaySettings"));

// cấu hình gửi request header trong swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurant API", Version = "v1" });

    // Cấu hình thêm JWT Bearer Token vào Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT token vào ô bên dưới. Ví dụ: Bearer your_token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Khởi tạo dữ liệu mẫu
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        dbContext.Database.Migrate();
        DatabaseInitializer.Initialize(dbContext);
        Console.WriteLine("Khởi tạo cơ sở dữ liệu thành công.");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Không thể khởi tạo cơ sở dữ liệu message: {e.Message}");
        Console.WriteLine($"Không thể khởi tạo cơ sở dữ liệu inner: {e.InnerException}");
    }
}

app.UseCors("AllowAll");

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
