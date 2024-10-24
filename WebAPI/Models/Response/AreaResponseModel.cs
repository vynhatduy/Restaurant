using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Response
{
    public class AreaResponseModel
    {
        public Guid IdKhuVuc { get; set; }

        public string TenKhuVuc { get; set; } 

        public string? MoTa { get; set; } 

        public bool? TrangThai { get; set; }
        public  List<TableResponseModel> DsBan { get; set; }
    }
}
