namespace NhaHang.Models.Request
{
    public class RegistRequestModel
    {

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Sdt { get; set; }

        public string HoTen { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
