using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Request
{
    public class OrderDetailRequestModel
    {

        public Guid IdSanPham { get; set; }

        public int? SoLuong { get; set; }
        public decimal? GiaBan { get; set; }
        public bool? TrangThai { get; set; }
    }
}
