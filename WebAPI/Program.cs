using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NhaHang.Helpers;
using System.IO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lấy thông tin kết nối từ biến môi trường
var server = Environment.GetEnvironmentVariable("DbServer") ?? "RestaurantDB"; // Tên container
var port = Environment.GetEnvironmentVariable("DbPort") ?? "1433";
var user = Environment.GetEnvironmentVariable("DbUser") ?? "SA";
var password = Environment.GetEnvironmentVariable("DbPassword") ?? "Restaurants@@";
var database = Environment.GetEnvironmentVariable("Database") ?? "NhaHangDB";

var connect = $"Server={server},{port};Initial Catalog={database};User ID={user};Password={password};TrustServerCertificate=True";

// Thêm Db context như một dịch vụ cho ứng dụng
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connect);
});

// Cấu hình JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(otp =>
    {
        otp.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Cấu hình chính sách ủy quyền
builder.Services.AddAuthorization(otp =>
{
    otp.AddPolicy("AdminPolicy", policy => policy.RequireRole("Administrator"));
    otp.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Management"));
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
// Khởi tạo dữ liệu mẫu
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


    DatabaseInitializer.Initialize(dbContext);
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
