using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Request
{
    public class EmployeeRequestModel
    {

        public string Username { get; set; } 

        public string Password { get; set; } 

        public string SDT { get; set; }

        public string HoTen { get; set; }
    }
}
