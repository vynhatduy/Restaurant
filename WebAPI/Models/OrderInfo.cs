namespace NhaHang.Models
{
    public class OrderInfo
    {
        public string IdHoaDon { get; set; }
        public Guid IdNhanVien { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal ThanhTien { get; set; }
        public string MoTa { get; set; }
        public string SoBan { get; set; }
    }
}
