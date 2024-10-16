namespace NhaHang.Models.Response
{
    public class OrderDetailResponseModel
    {
        public Guid IdHoaDon { get; set; }
        public Guid? IdNhanVien { get; set; }
        public string SoBan { get; set; }
        public List<ProductResponseModel> DsSanPham { get; set; }
        public decimal? TongTien { get; set; }
    }
}
