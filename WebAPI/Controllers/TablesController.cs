using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private ApplicationDbContext _context;
        public TablesController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
