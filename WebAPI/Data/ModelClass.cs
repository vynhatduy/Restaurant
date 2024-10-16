using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PhanQuyen
{
    [Key]
    public Guid IdQuyen { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string TenQuyen { get; set; } // Bắt buộc

    [StringLength(255)]
    public string? MoTa { get; set; } // Có thể null
    public ICollection<NhanVien> NhanViens { get; set; }
}

public class DanhMuc
{
    [Key]
    public Guid IdDanhMuc { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string TenDanhMuc { get; set; } // Bắt buộc

    [StringLength(255)]
    public string? MoTa { get; set; } // Có thể null

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật, mặc định là ngày hiện tại
    public ICollection<Loai> Loais { get; set; }
}

public class Loai
{
    [ForeignKey("DanhMuc")]
    public Guid IdDanhMuc { get; set; } // Khóa ngoại liên kết với bảng DanhMuc

    [Key]
    public Guid IdLoai { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string TenLoai { get; set; } // Bắt buộc

    [StringLength(255)]
    public string? MoTa { get; set; } // Có thể null

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật, mặc định là ngày hiện tại

    public virtual DanhMuc DanhMuc { get; set; } // Navigation property (nếu cần thiết)
    public ICollection<SanPham> SanPhams { get; set; }
}

public class NhanVien
{
    [Key]
    public Guid IdNhanVien { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string Username { get; set; } // Bắt buộc

    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } // Bắt buộc

    [Required]
    [StringLength(255)]
    public string Salt { get; set; } // Bắt buộc

    [StringLength(15)]
    public string SDT { get; set; } // Có thể null và không lặp lại

    [StringLength(255)]
    public string HoTen { get; set; } // Có thể null

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    [ForeignKey("PhanQuyen")]
    public Guid? IdQuyen { get; set; } // Khóa ngoại liên kết với bảng PhanQuyen

    public virtual PhanQuyen PhanQuyen { get; set; } // Navigation property (nếu cần thiết)
    public ICollection<HoaDon> HoasDons { get; set; }
}

public class SanPham
{
    [ForeignKey("Loai")]
    public Guid IdLoai { get; set; } // Khóa ngoại liên kết với bảng Loai

    [Key]
    public Guid IdSanPham { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string TenSanPham { get; set; } // Bắt buộc

    [Required]
    [StringLength(255)]
    public string? MoTa { get; set; } // Bắt buộc

    [StringLength(500)]
    public string? MoTaChiTiet { get; set; } // Có thể null

    [Required]
    [StringLength(255)]
    public string HinhAnh { get; set; } // Bắt buộc

    public bool? TrangThai { get; set; } // Kiểu boolean

    public decimal? DonGia { get; set; } // Giá sản phẩm

    public float? SoSao { get; set; } // Đánh giá sao

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật, mặc định là ngày hiện tại

    public virtual Loai Loai { get; set; } // Navigation property (nếu cần thiết)
    public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    public ICollection<ChiTietCombo> ChiTietCombos { get; set; }
    public ICollection<DanhGia> DanhGias { get; set; }
}

public class KhuVuc
{
    [Key]
    public Guid IdKhuVuc { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string TenKhuVuc { get; set; } // Bắt buộc

    [StringLength(255)]
    public string? MoTa { get; set; } // Có thể null

    public bool? TrangThai { get; set; } // Kiểu boolean

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật, mặc định là ngày hiện tại
    public ICollection<Ban> Bans { get; set; }
}

public class KhachHang
{
    [Key]
    public Guid IdKhachHang { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string Username { get; set; } // Bắt buộc

    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } // Bắt buộc

    [Required]
    [StringLength(255)]
    public string Salt { get; set; } // Bắt buộc

    [Required]
    [StringLength(15)]
    public string SDT { get; set; } // Không lặp lại và bắt buộc

    [StringLength(255)]
    public string HoTen { get; set; } // Có thể null

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    [StringLength(255)]
    public string? DiaChi { get; set; } // Có thể null

    public bool? GioiTinh { get; set; } // Kiểu boolean
    public ICollection<HoaDon> HoaDons { get; set; }
    public ICollection<DanhGia> DanhGias { get; set; }
}

public class Ban
{
    [Key]
    public Guid IdBan { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string SoBan { get; set; } // Bắt buộc

    public int SoChoNgoi { get; set; } // Số chỗ ngồi

    public bool? TrangThai { get; set; } // Kiểu boolean

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime? ThoiGianDatBan { get; set; } // Thời gian đặt bàn

    public DateTime? ThoiGianTraBan { get; set; } // Thời gian trả bàn

    [ForeignKey("KhuVuc")]
    public Guid? IdKhuVuc { get; set; } // Khóa ngoại liên kết với bảng KhuVuc

    public virtual KhuVuc KhuVuc { get; set; } // Navigation property (nếu cần thiết)
    public ICollection<HoaDon> HoaDons { get; set; }
}

public class HoaDon
{
    [Key]
    public Guid IdHoaDon { get; set; } // Định dạng GUID

    [ForeignKey("NhanVien")]
    public Guid? IdNhanVien { get; set; } // Khóa ngoại liên kết với bảng NhanVien

    [ForeignKey("Ban")]
    public Guid? IdBan { get; set; } // Khóa ngoại liên kết với bảng Ban

    [ForeignKey("KhachHang")]
    public Guid? IdKhachHang { get; set; } // Khóa ngoại liên kết với bảng KhachHang

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật, mặc định là ngày hiện tại

    [StringLength(255)]
    public string? ChietKhau { get; set; } // Có thể null

    [StringLength(255)]
    public string? Coupon { get; set; } // Có thể null

    public decimal? TongHoaDon { get; set; } // Tổng hóa đơn

    public decimal? ThanhTien { get; set; } // Thành tiền

    public bool? TrangThai { get; set; } // Kiểu boolean

    public bool? DaThanhToan { get; set; } // Kiểu boolean

    [Required]
    [StringLength(255)]
    public string PhuongThucThanhToan { get; set; } // Bắt buộc

    public virtual NhanVien NhanVien { get; set; } // Navigation property (nếu cần thiết)

    public virtual Ban Ban { get; set; } // Navigation property (nếu cần thiết)

    public virtual KhachHang KhachHang { get; set; } // Navigation property (nếu cần thiết)
    public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
}

public class ChiTietHoaDon
{
    [Key]
    public Guid IdChiTietHoaDon { get; set; } // Định dạng GUID

    [ForeignKey("HoaDon")]
    public Guid? IdHoaDon { get; set; } // Khóa ngoại liên kết với bảng HoaDon

    [ForeignKey("SanPham")]
    public Guid? IdSanPham { get; set; } // Khóa ngoại liên kết với bảng SanPham

    public decimal? GiaBan { get; set; } // Giá bán

    public int? SoLuong { get; set; } // Số lượng
    public bool? TrangThai { get; set; }
    public decimal? ThanhTien { get; set; } 

    public virtual HoaDon HoaDon { get; set; } // Navigation property (nếu cần thiết)

    public virtual SanPham SanPham { get; set; } // Navigation property (nếu cần thiết)
}
public class DanhGia
{
    [Key]
    public Guid IdDanhGia { get; set; } // Định dạng GUID

    [ForeignKey("KhachHang")]
    public Guid IdKhachHang { get; set; } // Khóa ngoại liên kết với bảng KhachHang

    [ForeignKey("SanPham")]
    public Guid IdSanPham { get; set; } // Khóa ngoại liên kết với bảng SanPham

    public float? SoSao { get; set; } // Đánh giá sao

    [StringLength(100)]
    public string? TieuDe { get; set; }
    [StringLength(1000)]
    public string? MoTa { get; set; } // Mô tả đánh giá
    [StringLength(255)]
    
    public string HinhAnh { get; set; }

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại
    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public virtual KhachHang KhachHang { get; set; } // Navigation property

    public virtual SanPham SanPham { get; set; } // Navigation property
}
public class Banner
{
    [Key]
    public Guid IdBanner { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string HinhAnh { get; set; } // Bắt buộc

    [StringLength(255)]
    public string Link { get; set; } // Link dẫn đến banner, có thể null

    public bool? TrangThai { get; set; } // Kiểu boolean

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật, mặc định là ngày hiện tại
}
public class Combo
{
    [Key]
    public Guid IdCombo { get; set; } // Định dạng GUID

    [Required]
    [StringLength(255)]
    public string TenCombo { get; set; } // Bắt buộc

    public decimal? Gia { get; set; } // Giá combo

    [StringLength(1000)]
    public string? MoTa { get; set; } // Mô tả combo

    public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

    public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật, mặc định là ngày hiện tại
    public ICollection<ChiTietCombo> ChiTietCombos { get; set; }
}
public class ChiTietCombo
{
    [Key]
    public Guid IdChiTietCombo { get; set; } // Định dạng GUID

    [ForeignKey("Combo")]
    public Guid IdCombo { get; set; } // Khóa ngoại liên kết với bảng Combo

    [ForeignKey("SanPham")]
    public Guid IdSanPham { get; set; } // Khóa ngoại liên kết với bảng SanPham

    public virtual Combo Combo { get; set; } // Navigation property liên kết với bảng Combo

    public virtual SanPham SanPham { get; set; } // Navigation property liên kết với bảng SanPham
}