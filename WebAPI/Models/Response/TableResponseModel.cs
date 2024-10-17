using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Response
{
    public class TableResponseModel
    {
        public Guid IdBan { get; set; } 

        public string SoBan { get; set; } 

        public int SoChoNgoi { get; set; } 

        public bool? TrangThai { get; set; } 

    }
}
