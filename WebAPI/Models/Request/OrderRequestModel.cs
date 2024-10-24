using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Request
{
    public class OrderRequestModel
    {

        public Guid? IdBan { get; set; }

        public Guid? IdKhachHang { get; set; }

        public string? ChietKhau { get; set; }

        public string? Coupon { get; set; }

        public bool? TrangThai { get; set; } 

        public bool? DaThanhToan { get; set; } 

        public string PhuongThucThanhToan { get; set; }
        public List<OrderDetailRequestModel> DsChiTietHoaDon { get; set; }
    }
}
