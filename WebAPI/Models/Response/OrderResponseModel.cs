namespace NhaHang.Models.Response
{
    public class OrderResponseModel
    {
        public Guid IdHoaDon { get; set; }
        public Guid? IdNhanVien { get; set; }
        public string SoBan { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal? TongTien { get; set; }
        public bool? TrangThai { get; set; }
        public bool? DaThanhToan { get; set; }
    }
}
