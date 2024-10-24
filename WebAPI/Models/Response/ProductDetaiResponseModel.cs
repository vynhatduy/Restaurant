
namespace NhaHang.Models.Response
{
    public class ProductDetaiResponseModel
    {
        public string HinhAnh { get; set; }
        public string IdSanPham { get; set; }
        public string Loai { get; set; }
        public string DanhMuc { get; set; }
        public string MoTa { get; set; }
        public string MoTaChiTiet { get; set; }
        public decimal DonGia { get; set; }
        public float SoSao { get; set; }
        public List<DanhGia> DanhGias { get; set; }
    }
}
