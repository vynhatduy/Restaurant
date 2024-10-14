

namespace NhaHang.Models.Response
{
    public class ProductResponseModel
    {
        public string HinhAnh { get; set; }
        public string IdSanPham { get; set; }
        public string Loai { get; set; }
        public string MoTa { get; set; }
        public decimal DonGia { get; set; }
        public float SoSao { get; set; }
        public int SoLuong { get; set; }

        public bool TrangThai { get; set; }

    }
}
