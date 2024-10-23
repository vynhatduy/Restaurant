using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Request;
using WebAPI.Models.Response;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        private ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("All")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_context.NhanViens.Select(x=> new EmployeeResponseModel
                {
                    IdNhanVien=x.IdNhanVien,
                    HoTen=x.HoTen,
                    IdQuyen=x.IdQuyen,
                    NgayTao=x.NgayTao,
                    PasswordHash=x.PasswordHash,
                    Salt=x.Salt,
                    SDT=x.SDT,
                    Username=x.Username
                }).ToList());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("ById{IdNhanVien:guid}")]
        public IActionResult GetById(Guid IdNhanVien)
        {
            try
            {
                return Ok(_context.NhanViens.Where(x=>x.IdNhanVien==IdNhanVien).Select(x => new EmployeeResponseModel
                {
                    IdNhanVien = x.IdNhanVien,
                    HoTen = x.HoTen,
                    IdQuyen = x.IdQuyen,
                    NgayTao = x.NgayTao,
                    PasswordHash = x.PasswordHash,
                    Salt = x.Salt,
                    SDT = x.SDT,
                    Username = x.Username
                }).FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("ByName{HoTen:string}")]
        public IActionResult GetByName(string HoTen)
        {
            try
            {
                return Ok(_context.NhanViens.Where(x=>x.HoTen.ToLower().Contains(HoTen.ToLower())).Select(x => new EmployeeResponseModel
                {
                    IdNhanVien = x.IdNhanVien,
                    HoTen = x.HoTen,
                    IdQuyen = x.IdQuyen,
                    NgayTao = x.NgayTao,
                    PasswordHash = x.PasswordHash,
                    Salt = x.Salt,
                    SDT = x.SDT,
                    Username = x.Username
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("ByIdRole{IdQuyen:guid}")]
        public IActionResult GetByIdRole(Guid IdQuyen)
        {
            try
            {
                return Ok(_context.NhanViens.Where(x=>x.IdQuyen==IdQuyen).Select(x => new EmployeeResponseModel
                {
                    IdNhanVien = x.IdNhanVien,
                    HoTen = x.HoTen,
                    IdQuyen = x.IdQuyen,
                    NgayTao = x.NgayTao,
                    PasswordHash = x.PasswordHash,
                    Salt = x.Salt,
                    SDT = x.SDT,
                    Username = x.Username
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("Create")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult Create(EmployeeRequestModel model)
        {
            try
            {
                var user = _context.NhanViens.FirstOrDefault(x => x.Username == model.Username);
                if(user != null)
                {
                    return BadRequest(new {Message="Tên đăng nhập đã tồn tại!" });
                }
                var hash = HasherPassword.HasherPass(model.Password);
                var newUser=new NhanVien
                {
                    IdNhanVien=Guid.NewGuid(),
                    HoTen=model.HoTen,
                    SDT=model.SDT,
                    PasswordHash=hash.Hash,
                    Salt=hash.Salt,
                    Username=model.Username,
                    IdQuyen=_context.PhanQuyens.FirstOrDefault(x=>x.TenQuyen=="User").IdQuyen,
                    NgayTao=DateTime.Now
                };
                _context.NhanViens.Add(newUser);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("Update/{Username:string}/{TenQuyen:string}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult Update(string Username,string TenQuyen)
        {
            try
            {
                var user = _context.NhanViens.FirstOrDefault(x => x.Username == Username);
                if(user == null)
                {
                    return BadRequest(new {Message="Người dùng không tồn tại!" });
                }
                user.IdQuyen = _context.PhanQuyens.FirstOrDefault(x => x.TenQuyen == TenQuyen).IdQuyen;
                
                _context.NhanViens.Update(user);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("Delete/{Username:string}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult Delete(string Username)
        {
            try
            {
                var user = _context.NhanViens.FirstOrDefault(x => x.Username == Username);
                if(user == null)
                {
                    return BadRequest(new {Message="Người dùng không tồn tại!" });
                }
                
                _context.NhanViens.Remove(user);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}
