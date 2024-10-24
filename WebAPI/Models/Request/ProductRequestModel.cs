namespace NhaHang.Models.Request
{
    public class ProductRequestModel
    {
        public string TenLoai { get; set; }


        public string TenSanPham { get; set; }

        public string MoTa { get; set; }

        public string MoTaChiTiet { get; set; }

        public string HinhAnh { get; set; } = null!;

        public bool TrangThai { get; set; }

        public decimal DonGia { get; set; }

        public double SoSao { get; set; }

    }
}
