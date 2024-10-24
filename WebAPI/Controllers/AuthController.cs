using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; 
using NhaHang.Helpers;
using NhaHang.Models.Request;

namespace NhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequestModel model)
        {
            var user = _context.NhanViens.FirstOrDefault(x => x.Username == model.Username);
            if (user != null)
            {
                if (HasherPassword.VerifyPassword(model.Password, user.PasswordHash, user.Salt))
                {
                    try
                    {
                        var jwtKey = _configuration["Jwt:Key"];
                        var jwtIssuer = _configuration["Jwt:Issuer"];
                        var jwtAudience = _configuration["Jwt:Audience"];
                        var tenQuyen = _context.PhanQuyens.FirstOrDefault(x => x.IdQuyen == user.IdQuyen).TenQuyen??"User";

                        var tokenGenerator = new GenerateToken(jwtKey, jwtIssuer, jwtAudience);
                        var token = tokenGenerator.GenerateEmployeeToken(user,tenQuyen);

                        return Ok(new { Token = token });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Có lỗi xảy ra: {ex.Message}");
                    }
                }
                return Unauthorized("Mật khẩu không đúng");
            }

            var customer = _context.KhachHangs.FirstOrDefault(x => x.Username == model.Username);
            if (customer != null)
            {
                if (HasherPassword.VerifyPassword(model.Password, customer.PasswordHash, customer.Salt))
                {
                    try
                    {
                        var jwtKey = _configuration["Jwt:Key"];
                        var jwtIssuer = _configuration["Jwt:Issuer"];
                        var jwtAudience = _configuration["Jwt:Audience"];

                        var tokenGenerator = new GenerateToken(jwtKey, jwtIssuer, jwtAudience);
                        var token = tokenGenerator.GenerateCustomerToken(customer);

                        return Ok(new { Token = token });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Có lỗi xảy ra: {ex.Message}");
                    }
                }
                return Unauthorized("Mật khẩu không đúng");
            }

            return NotFound("Tên đăng nhập không tồn tại");
        }

        [HttpPost("RegistUser")]
        public IActionResult RegistUser(RegistRequestModel model)
        {
            try
            {
                var item = _context.NhanViens.FirstOrDefault(x => x.Username == model.Username);
                if (item != null)
                {
                    return BadRequest("Tên đăng nhập đã tồn tại");
                }
                var haser = HasherPassword.HasherPass(model.Password);
                var newUser = new NhanVien
                {
                    IdNhanVien = Guid.NewGuid(),
                    Username = model.Username,
                    HoTen = model.HoTen,
                    IdQuyen = _context.PhanQuyens.FirstOrDefault(x => x.TenQuyen == "User").IdQuyen,
                    NgayTao = DateTime.Now,
                    SDT = model.Sdt,
                    PasswordHash = haser.Hash,
                    Salt = haser.Salt
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
        [HttpPost("RegistCustomer")]
        public IActionResult RegistCustomer(RegistRequestModel model)
        {
            try
            {
                var item = _context.KhachHangs.FirstOrDefault(x => x.Username == model.Username);
                if (item != null)
                {
                    return BadRequest("Tên đăng nhập đã tồn tại");
                }
                var haser = HasherPassword.HasherPass(model.Password);
                var newUser = new KhachHang
                {
                    IdKhachHang = Guid.NewGuid(),
                    Username = model.Username,
                    HoTen = model.HoTen,
                    NgayTao = DateTime.Now,
                    SDT = model.Sdt,
                    PasswordHash = haser.Hash,
                    Salt = haser.Salt

                };
                _context.KhachHangs.Add(newUser);
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
