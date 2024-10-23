using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Response
{
    public class EmployeeResponseModel
    {
        public Guid IdNhanVien { get; set; } // Định dạng GUID

        public string Username { get; set; } // Bắt buộc

        public string PasswordHash { get; set; } // Bắt buộc

        public string Salt { get; set; } // Bắt buộc

        public string SDT { get; set; } // Có thể null và không lặp lại

        public string HoTen { get; set; } // Có thể null

        public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo, mặc định là ngày hiện tại

        public Guid? IdQuyen { get; set; } // Khóa ngoại liên kết với bảng PhanQuyen
    }
}
