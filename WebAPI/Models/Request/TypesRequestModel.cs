using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Request
{
    public class TypesRequestModel
    {
        public string TenLoai { get; set; } 

        public string MoTa { get; set; } 

    }
}
