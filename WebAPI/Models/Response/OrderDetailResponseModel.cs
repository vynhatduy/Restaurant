namespace NhaHang.Models.Response
{
    public class OrderDetailResponseModel
    {
        public string IdHoaDon { get; set; }
        public string IdNhanVien { get; set; }
        public string SoBan { get; set; }
        public List<ProductResponseModel> DsSanPham { get; set; }
        public decimal TongTien { get; set; }
    }
}
