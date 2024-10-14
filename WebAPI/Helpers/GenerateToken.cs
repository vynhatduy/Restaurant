using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NhaHang.Helpers
{
    public class GenerateToken
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public GenerateToken(string key, string issuer, string audience)
        {
            _key = key;
            _issuer = issuer;
            _audience = audience;
        }

        public string GenerateEmployeeToken(NhanVien nv)
        {
            var claims = new[]
            {
                new Claim("IdNhanVien", nv.IdNhanVien.ToString()),
                new Claim("Sdt", nv.SDT ?? string.Empty),
                new Claim("HoTen", nv.HoTen ?? string.Empty),
                new Claim("IdQuyen", nv.IdQuyen?.ToString() ?? string.Empty),
                new Claim("TenQuyen", nv.PhanQuyen.TenQuyen ?? string.Empty)
            };

            return CreateToken(claims);
        }

        public string GenerateCustomerToken(KhachHang kh)
        {
            var claims = new[]
            {
                new Claim("IdKhachHang", kh.IdKhachHang.ToString()),
                new Claim("Sdt", kh.SDT ?? string.Empty),
                new Claim("HoTen", kh.HoTen ?? string.Empty),
            };

            return CreateToken(claims);
        }

        private string CreateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token sẽ hết hạn sau 1 giờ
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
