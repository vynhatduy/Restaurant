# Chọn hình ảnh cơ bản ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

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
RUN dotnet build "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish ứng dụng
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Tạo container cuối cùng
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Định nghĩa điểm vào của ứng dụng
ENTRYPOINT ["dotnet", "WebAPI.dll"]
