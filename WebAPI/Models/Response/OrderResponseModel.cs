namespace NhaHang.Models.Response
{
    public class OrderResponseModel
    {
        public string IdHoaDon { get; set; }
        public string IdNhanVien { get; set; }
        public string SoBan { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal TongTien { get; set; }
        public bool TrangThai { get; set; }
        public bool DaThanhToan { get; set; }
    }
}
