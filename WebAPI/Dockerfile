# Chọn hình ảnh cơ bản ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443 

# Thiết lập biến môi trường để sử dụng HTTPS với chứng chỉ
ENV ASPNETCORE_URLS=https://+:443;http://+:8080;
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=vynhatduy11092003
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# Sử dụng hình ảnh SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Sao chép file csproj và khôi phục các gói NuGet
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
RUN dotnet restore "./WebAPI/WebAPI.csproj"

# Sao chép toàn bộ mã nguồn
COPY . .

# Chuyển đến thư mục chứa dự án và xây dựng ứng dụng
WORKDIR "/src/WebAPI"
RUN dotnet build "./WebAPI.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Publish ứng dụng
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebAPI.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish /p:UseAppHost=false

# Tạo container cuối cùng
FROM base AS final
WORKDIR /app

# Sao chép chứng chỉ HTTPS vào container
COPY "./WebAPI/https/aspnetapp.pfx" /https/aspnetapp.pfx

# Sao chép ứng dụng đã publish vào container
COPY --from=publish /app/publish .

# Định nghĩa điểm vào của ứng dụng
ENTRYPOINT ["dotnet", "WebAPI.dll"]
