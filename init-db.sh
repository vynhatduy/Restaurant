#!/bin/bash

# Đợi SQL Server khởi động
sleep 30s

# Tạo cơ sở dữ liệu nếu chưa tồn tại
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Restaurants@@" -d master -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'NhaHangDB') CREATE DATABASE NhaHangDB"

# Khởi tạo dữ liệu mẫu (nếu cần)
# Bạn có thể thêm lệnh khởi tạo dữ liệu mẫu vào đây, nếu bạn có tệp SQL để thực thi
