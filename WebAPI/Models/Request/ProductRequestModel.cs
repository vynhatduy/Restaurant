namespace NhaHang.Models.Request
{
    public class ProductRequestModel
    {
        public Guid IdSanPham { get; set; }
        public Guid IdLoai { get; set; }


        public string TenSanPham { get; set; }

        public string MoTa { get; set; }

        public string MoTaChiTiet { get; set; }

        public string HinhAnh { get; set; } = null!;

        public bool TrangThai { get; set; }

        public decimal DonGia { get; set; }

        public double SoSao { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }
    }
}
