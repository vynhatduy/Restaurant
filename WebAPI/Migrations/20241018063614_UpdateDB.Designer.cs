﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241018063614_UpdateDB")]
    partial class UpdateDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ban", b =>
                {
                    b.Property<Guid>("IdBan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdKhuVuc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoBan")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SoChoNgoi")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ThoiGianDatBan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThoiGianTraBan")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("IdBan");

                    b.HasIndex("IdKhuVuc");

                    b.ToTable("Bans");
                });

            modelBuilder.Entity("Banner", b =>
                {
                    b.Property<Guid>("IdBanner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("IdBanner");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("ChiTietCombo", b =>
                {
                    b.Property<Guid>("IdChiTietCombo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdCombo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdChiTietCombo");

                    b.HasIndex("IdCombo");

                    b.HasIndex("IdSanPham");

                    b.ToTable("ChiTietCombos");
                });

            modelBuilder.Entity("ChiTietHoaDon", b =>
                {
                    b.Property<Guid>("IdChiTietHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("GiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("IdHoaDon")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<decimal?>("ThanhTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("IdChiTietHoaDon");

                    b.HasIndex("IdHoaDon");

                    b.HasIndex("IdSanPham");

                    b.ToTable("ChiTietHoaDons");
                });

            modelBuilder.Entity("Combo", b =>
                {
                    b.Property<Guid>("IdCombo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MoTa")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenCombo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdCombo");

                    b.ToTable("Combos");
                });

            modelBuilder.Entity("DanhGia", b =>
                {
                    b.Property<Guid>("IdDanhGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("IdKhachHang")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<float?>("SoSao")
                        .HasColumnType("real");

                    b.Property<string>("TieuDe")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdDanhGia");

                    b.HasIndex("IdKhachHang");

                    b.HasIndex("IdSanPham");

                    b.ToTable("DanhGias");
                });

            modelBuilder.Entity("DanhMuc", b =>
                {
                    b.Property<Guid>("IdDanhMuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenDanhMuc")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdDanhMuc");

                    b.ToTable("DanhMucs");
                });

            modelBuilder.Entity("HoaDon", b =>
                {
                    b.Property<Guid>("IdHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChietKhau")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Coupon")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("DaThanhToan")
                        .HasColumnType("bit");

                    b.Property<Guid?>("IdBan")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdKhachHang")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdNhanVien")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhuongThucThanhToan")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("ThanhTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TongHoaDon")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("IdHoaDon");

                    b.HasIndex("IdBan");

                    b.HasIndex("IdKhachHang");

                    b.HasIndex("IdNhanVien");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("KhachHang", b =>
                {
                    b.Property<Guid>("IdKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdKhachHang");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("KhuVuc", b =>
                {
                    b.Property<Guid>("IdKhuVuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenKhuVuc")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("IdKhuVuc");

                    b.ToTable("KhuVucs");
                });

            modelBuilder.Entity("Loai", b =>
                {
                    b.Property<Guid>("IdLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdDanhMuc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdLoai");

                    b.HasIndex("IdDanhMuc");

                    b.ToTable("Loais");
                });

            modelBuilder.Entity("NhanVien", b =>
                {
                    b.Property<Guid>("IdNhanVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("IdQuyen")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdNhanVien");

                    b.HasIndex("IdQuyen");

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("PhanQuyen", b =>
                {
                    b.Property<Guid>("IdQuyen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenQuyen")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdQuyen");

                    b.ToTable("PhanQuyens");
                });

            modelBuilder.Entity("SanPham", b =>
                {
                    b.Property<Guid>("IdSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("IdLoai")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MoTaChiTiet")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<float?>("SoSao")
                        .HasColumnType("real");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("IdSanPham");

                    b.HasIndex("IdLoai");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("Ban", b =>
                {
                    b.HasOne("KhuVuc", "KhuVuc")
                        .WithMany("Bans")
                        .HasForeignKey("IdKhuVuc");

                    b.Navigation("KhuVuc");
                });

            modelBuilder.Entity("ChiTietCombo", b =>
                {
                    b.HasOne("Combo", "Combo")
                        .WithMany("ChiTietCombos")
                        .HasForeignKey("IdCombo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SanPham", "SanPham")
                        .WithMany("ChiTietCombos")
                        .HasForeignKey("IdSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Combo");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ChiTietHoaDon", b =>
                {
                    b.HasOne("HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("IdHoaDon");

                    b.HasOne("SanPham", "SanPham")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("IdSanPham");

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DanhGia", b =>
                {
                    b.HasOne("KhachHang", "KhachHang")
                        .WithMany("DanhGias")
                        .HasForeignKey("IdKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SanPham", "SanPham")
                        .WithMany("DanhGias")
                        .HasForeignKey("IdSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("HoaDon", b =>
                {
                    b.HasOne("Ban", "Ban")
                        .WithMany("HoaDons")
                        .HasForeignKey("IdBan");

                    b.HasOne("KhachHang", "KhachHang")
                        .WithMany("HoaDons")
                        .HasForeignKey("IdKhachHang");

                    b.HasOne("NhanVien", "NhanVien")
                        .WithMany("HoasDons")
                        .HasForeignKey("IdNhanVien");

                    b.Navigation("Ban");

                    b.Navigation("KhachHang");

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("Loai", b =>
                {
                    b.HasOne("DanhMuc", "DanhMuc")
                        .WithMany("Loais")
                        .HasForeignKey("IdDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DanhMuc");
                });

            modelBuilder.Entity("NhanVien", b =>
                {
                    b.HasOne("PhanQuyen", "PhanQuyen")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdQuyen");

                    b.Navigation("PhanQuyen");
                });

            modelBuilder.Entity("SanPham", b =>
                {
                    b.HasOne("Loai", "Loai")
                        .WithMany("SanPhams")
                        .HasForeignKey("IdLoai")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loai");
                });

            modelBuilder.Entity("Ban", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("Combo", b =>
                {
                    b.Navigation("ChiTietCombos");
                });

            modelBuilder.Entity("DanhMuc", b =>
                {
                    b.Navigation("Loais");
                });

            modelBuilder.Entity("HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("KhachHang", b =>
                {
                    b.Navigation("DanhGias");

                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("KhuVuc", b =>
                {
                    b.Navigation("Bans");
                });

            modelBuilder.Entity("Loai", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("NhanVien", b =>
                {
                    b.Navigation("HoasDons");
                });

            modelBuilder.Entity("PhanQuyen", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("SanPham", b =>
                {
                    b.Navigation("ChiTietCombos");

                    b.Navigation("ChiTietHoaDons");

                    b.Navigation("DanhGias");
                });
#pragma warning restore 612, 618
        }
    }
}
