using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NhaHang.Models.Request;

namespace NhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("AllProduct")]
        public IActionResult GetProducts()
        {
            try
            {
                return Ok(_context.SanPhams.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            try
            {
                return Ok(_context.DanhMucs.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Types")]
        public IActionResult GetTypeProduct()
        {
            try
            {
                return Ok(_context.Loais.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Product/{IdSanPham:guid}")]
        public IActionResult GetById(Guid IdSanPham)
        {
            try
            {
                var item = _context.SanPhams.FirstOrDefault(x => x.IdSanPham == IdSanPham);
                return item != null ? Ok(item) : NotFound("Không tìm thấy sản phẩm");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Product/Type/{IdLoai:guid}")]
        public IActionResult GetByTypeProduct(Guid IdLoai)
        {
            try
            {
                var items = _context.SanPhams.Where(x => x.IdLoai == IdLoai).ToList();
                return items != null && items.Count > 0 ? Ok(items) : NotFound("Không tìm thấy sản phẩm");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Product/Name/{TenSanPham}")]
        public IActionResult GetByName(string TenSanPham)
        {
            try
            {
                var items = _context.SanPhams.Where(x => x.TenSanPham.ToLower().Contains(TenSanPham.ToLower())).ToList();
                return items != null && items.Count > 0 ? Ok(items) : NotFound("Không tìm thấy sản phẩm");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult AddProduct(ProductRequestModel model)
        {
            try
            {
                if (model != null)
                {

                    var newId = Guid.NewGuid();
                    var newProduct = new SanPham
                    {
                        IdSanPham = newId,
                        IdLoai = model.IdLoai,
                        TrangThai = true,
                        HinhAnh = model.HinhAnh,
                        MoTa = model.MoTa,
                        NgayCapNhat = DateTime.Now,
                        NgayTao = DateTime.Now,
                        SoSao = 0,
                        MoTaChiTiet = model.MoTaChiTiet,
                        DonGia = model.DonGia,
                        TenSanPham = model.TenSanPham
                    };
                    _context.SanPhams.Add(newProduct);
                    _context.SaveChangesAsync();
                    return Ok("Đã thêm sản phẩm mới");
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrator,Management")]
        public async Task<IActionResult> UpdateProduct(ProductRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    var curProduct = _context.SanPhams.FirstOrDefault(x => x.IdSanPham == model.IdSanPham);
                    if (curProduct != null)
                    {
                        curProduct.IdLoai = model.IdLoai;
                        curProduct.TrangThai = model.TrangThai;
                        curProduct.HinhAnh = model.HinhAnh;
                        curProduct.MoTa = model.MoTa;
                        curProduct.MoTaChiTiet = model.MoTaChiTiet;
                        curProduct.NgayCapNhat = DateTime.Now;
                        curProduct.DonGia = model.DonGia;
                        curProduct.TenSanPham = model.TenSanPham;

                        _context.SanPhams.Update(curProduct);
                        await _context.SaveChangesAsync();

                        return Ok("Đã cập nhật sản phẩm thành công");
                    }
                    return NotFound("Sản phẩm không tồn tại");
                }
                return BadRequest("Thông tin sản phẩm không hợp lệ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{IdSanPham:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult DeleteProduct(Guid IdSanPham)
        {
            try
            {
                var item = _context.SanPhams.FirstOrDefault(x => x.IdSanPham == IdSanPham);
                if (item != null)
                {
                    _context.SanPhams.Remove(item);
                    _context.SaveChanges();
                    return Ok($"Đã xóa sản phẩm có id {IdSanPham}");
                }
                return BadRequest("Không tìm thấy sản phẩm");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}