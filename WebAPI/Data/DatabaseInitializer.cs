using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Xóa cơ sở dữ liệu nếu tồn tại
        context.Database.EnsureDeleted();

        // Tạo lại cơ sở dữ liệu
        context.Database.EnsureCreated();

        if (!context.Banners.Any())
        {
            AddBanner(context);
        }
        if (!context.PhanQuyens.Any())
        {
            AddPhanQuyen(context);
        }
        if (!context.DanhMucs.Any())
        {
            AddDanhMuc(context);
        }
        if (!context.Loais.Any())
        {
            AddLoai(context);
        }
        if (!context.NhanViens.Any())
        {
            AddNhanVien(context);
        }
        if (!context.KhuVucs.Any())
        {
            AddKhuVuc(context);
        }
        if (!context.KhachHangs.Any())
        {
            AddKhachHang(context);
        }
        if (!context.SanPhams.Any())
        {
            AddSanPham(context);
        }
        if (!context.HoaDons.Any())
        {
            AddHoaDon(context);
        }
        if (!context.DanhGias.Any())
        {
            AddDanhGia(context);
        }
        if (!context.ChiTietHoaDons.Any())
        {
            AddChiTietHoaDon(context);
        }


    }
    public static void AddBanner(ApplicationDbContext context)
    {
        context.Banners.AddRange(
            new Banner
            {
                IdBanner = Guid.NewGuid(),
                HinhAnh = "Banner_1",
                Link = "#",
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now,
                TrangThai = true
            },
            new Banner
            {
                IdBanner = Guid.NewGuid(),
                HinhAnh = "Banner_2",
                Link = "#",
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now,
                TrangThai = true
            }
            );
        context.SaveChanges();
    }
    public static void AddPhanQuyen(ApplicationDbContext context)
    {
        context.PhanQuyens.AddRange(
                new PhanQuyen { IdQuyen = Guid.NewGuid(), TenQuyen = "Administrator", MoTa = "Quyền quản trị viên" },
                new PhanQuyen { IdQuyen = Guid.NewGuid(), TenQuyen = "Management", MoTa = "Quyền quản lý" },
                new PhanQuyen { IdQuyen = Guid.NewGuid(), TenQuyen = "User", MoTa = "Quyền người dùng" }
            );
        context.SaveChanges();
    }
    public static void AddDanhMuc(ApplicationDbContext context)
    {
        context.DanhMucs.AddRange(
                new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Thực phẩm", MoTa = "Danh mục thực phẩm", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Đồ uống", MoTa = "Danh mục đồ uống", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        context.SaveChanges();
    }  
    public static void AddLoai(ApplicationDbContext context)
    {
        var firstDanhMuc = context.DanhMucs.FirstOrDefault();
        if (firstDanhMuc != null)
        {
            context.Loais.AddRange(
                new Loai { IdDanhMuc = firstDanhMuc.IdDanhMuc, IdLoai = Guid.NewGuid(), TenLoai = "Trái cây", MoTa = "Danh mục trái cây", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new Loai { IdDanhMuc = firstDanhMuc.IdDanhMuc, IdLoai = Guid.NewGuid(), TenLoai = "Thịt", MoTa = "Danh mục thịt", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        }
        context.SaveChanges();
    }
    public static void AddNhanVien(ApplicationDbContext context)
    {
        var adminRole = context.PhanQuyens.FirstOrDefault(p => p.TenQuyen == "Administrator");
        var managerRole = context.PhanQuyens.FirstOrDefault(p => p.TenQuyen == "Management");
        var userRole = context.PhanQuyens.FirstOrDefault(p => p.TenQuyen == "User");

        var hasher1 = HasherPassword.HasherPass("admin");
        var hasher2 = HasherPassword.HasherPass("manager");
        var hasher3 = HasherPassword.HasherPass("user");

        var salt1 = hasher1.Salt;
        var pass1 = hasher1.Hash;
        var salt2 = hasher2.Salt;
        var pass2 = hasher2.Hash;
        var salt3 = hasher3.Salt;
        var pass3 = hasher3.Hash;

        if (adminRole != null && userRole != null)
        {
            context.NhanViens.AddRange(
                new NhanVien { IdNhanVien = Guid.NewGuid(), Username = "admin", PasswordHash = pass1, Salt = salt1, SDT = "0123456789", HoTen = "Nguyễn Văn A", NgayTao = DateTime.Now, IdQuyen = adminRole.IdQuyen },
                new NhanVien { IdNhanVien = Guid.NewGuid(), Username = "manager", PasswordHash = pass2, Salt = salt2, SDT = "0987654321", HoTen = "Trần Thị B", NgayTao = DateTime.Now, IdQuyen = managerRole.IdQuyen },
                new NhanVien { IdNhanVien = Guid.NewGuid(), Username = "user", PasswordHash = pass3, Salt = salt3, SDT = "0987654322", HoTen = "Trần Van C", NgayTao = DateTime.Now, IdQuyen = userRole.IdQuyen }
            );
        }
        context.SaveChanges();
    }
    public static void AddKhuVuc(ApplicationDbContext context)
    {
        context.KhuVucs.AddRange(
                new KhuVuc { IdKhuVuc = Guid.NewGuid(), TenKhuVuc = "Khu A", MoTa = "Khu vực chính", TrangThai = true, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new KhuVuc { IdKhuVuc = Guid.NewGuid(), TenKhuVuc = "Khu B", MoTa = "Khu vực phụ", TrangThai = true, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        context.SaveChanges();
    }
    public static void AddKhachHang(ApplicationDbContext context)
    {
        context.KhachHangs.AddRange(
                new KhachHang { IdKhachHang = Guid.NewGuid(), Username = "khach1", PasswordHash = "hashed_password_3", Salt = "salt_password_3", SDT = "0123456780", HoTen = "Lê Văn C", NgayTao = DateTime.Now, DiaChi = "Hà Nội", GioiTinh = true },
                new KhachHang { IdKhachHang = Guid.NewGuid(), Username = "khach2", PasswordHash = "hashed_password_4", Salt = "salt_password_4", SDT = "0987654310", HoTen = "Phạm Thị D", NgayTao = DateTime.Now, DiaChi = "Đà Nẵng", GioiTinh = false }
            );
        context.SaveChanges();
    }
    public static void AddSanPham(ApplicationDbContext context)
    {
        var firstLoai = context.Loais.FirstOrDefault();
        if (firstLoai != null)
        {
            context.SanPhams.AddRange(
                new SanPham { IdSanPham = Guid.NewGuid(), IdLoai = firstLoai.IdLoai, TenSanPham = "Táo", MoTa = "Táo tươi", MoTaChiTiet = "Táo ngon và bổ dưỡng", HinhAnh = "image_url_1", TrangThai = true, DonGia = 10000, SoSao = 4.5f, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new SanPham { IdSanPham = Guid.NewGuid(), IdLoai = context.Loais.First(l => l.TenLoai == "Thịt").IdLoai, TenSanPham = "Thịt heo", MoTa = "Thịt heo sạch", MoTaChiTiet = "Thịt heo tươi ngon", HinhAnh = "image_url_2", TrangThai = true, DonGia = 20000, SoSao = 4.0f, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        }
        context.SaveChanges();
    }
    public static void AddHoaDon(ApplicationDbContext context)
    {
        var firstNhanVien = context.NhanViens.FirstOrDefault();
        var firstKhachHang = context.KhachHangs.FirstOrDefault();
        if (firstNhanVien != null && firstKhachHang != null)
        {
            context.HoaDons.AddRange(
                new HoaDon { IdHoaDon = Guid.NewGuid(), IdNhanVien = firstNhanVien.IdNhanVien, IdBan = null, IdKhachHang = firstKhachHang.IdKhachHang, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now, ChietKhau = null, Coupon = null, TongHoaDon = 50000, ThanhTien = 50000, TrangThai = true, DaThanhToan = true, PhuongThucThanhToan = "Tiền mặt" }
            );
        }
        context.SaveChanges();
    }
    public static void AddDanhGia(ApplicationDbContext context)
    {
        var firstKhachHang = context.KhachHangs.FirstOrDefault();
        var firstSanPham = context.SanPhams.FirstOrDefault();
        if (firstKhachHang != null && firstSanPham != null)
        {
            context.DanhGias.AddRange(
                new DanhGia { IdDanhGia = Guid.NewGuid(), IdKhachHang = firstKhachHang.IdKhachHang, IdSanPham = firstSanPham.IdSanPham, MoTa = "Tuyệt vời, Sản phẩm rất tốt", SoSao = 5, HinhAnh = "image_url_review", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        }
        context.SaveChanges();
    }
    public static void AddChiTietHoaDon(ApplicationDbContext context)
    {
        var firstHoaDon = context.HoaDons.FirstOrDefault();
        var firstSanPham = context.SanPhams.FirstOrDefault();
        if (firstHoaDon != null && firstSanPham != null)
        {
            context.ChiTietHoaDons.AddRange(
                new ChiTietHoaDon { IdChiTietHoaDon = Guid.NewGuid(), IdHoaDon = firstHoaDon.IdHoaDon, IdSanPham = firstSanPham.IdSanPham, SoLuong = 2, GiaBan = firstSanPham.DonGia, ThanhTien = firstSanPham.DonGia * 2 }
            );
        }
        context.SaveChanges();
    }
}
