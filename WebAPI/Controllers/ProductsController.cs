using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NhaHang.Models.Request;
using WebAPI.Models.Request;

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
        [HttpGet("All")]
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

        [HttpGet("ById/{IdSanPham:guid}")]
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

        [HttpGet("ByType/{IdLoai:guid}")]
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

        [HttpGet("ByName/{TenSanPham}")]
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

        [HttpPost("Create")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult AddProduct(ProductRequestModel model)
        {
            try
            {
                if (model != null)
                {

                    var newId = Guid.NewGuid();
                    var loai = _context.Loais.FirstOrDefault(x => x.TenLoai.ToLower().Contains(model.TenLoai.ToLower()));
                    if (loai == null)
                    {
                        return NotFound(new { Message = "Không tìm thấy tên loại :" + model.TenLoai });
                    }
                    var IdLoai = loai.IdLoai;
                    var newProduct = new SanPham
                    {
                        IdSanPham = newId,
                        IdLoai = IdLoai,
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
                    _context.SaveChanges();
                    return Ok("Đã thêm sản phẩm mới");
                }
                return BadRequest(new { Message = $"Lỗi đối tượng truyền vào Loại: {model.TenLoai}, TenSanPham: {model.TenSanPham}, MoTa: {model.MoTa}, MoTaChiTiet: {model.MoTaChiTiet}, HinhAnh: {model.HinhAnh}, TrangThai: {model.TrangThai}, DonGia: {model.DonGia}, SoSao: {model.SoSao}" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
        [HttpPut("Update/{IdSanPham:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult UpdateProduct(Guid IdSanPham, ProductRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    var loai = _context.Loais.FirstOrDefault(x => x.TenLoai.ToLower().Contains(model.TenLoai.ToLower()));
                    if (loai == null)
                    {
                        return NotFound(new { Message = "Không tìm thấy tên loại :" + model.TenLoai });
                    }
                    var IdLoai = loai.IdLoai;
                    var curProduct = _context.SanPhams.FirstOrDefault(x => x.IdSanPham == IdSanPham);
                    if (curProduct != null)
                    {
                            curProduct.TrangThai = model.TrangThai;
                            curProduct.HinhAnh = model.HinhAnh;
                            curProduct.MoTa = model.MoTa;
                            curProduct.MoTaChiTiet = model.MoTaChiTiet;
                            curProduct.NgayCapNhat = DateTime.Now;
                            curProduct.DonGia = model.DonGia;
                            curProduct.TenSanPham = model.TenSanPham;

                            _context.SanPhams.Update(curProduct);
                            _context.SaveChanges();

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
        [HttpDelete("Delete/{IdSanPham:guid}")]
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
        [HttpPost("Categories")]
        [Authorize(Roles ="Administrator,Management")]
        public IActionResult AddCategories(CategoriesRequestModel model)
        {
            try
            {
                var item = _context.DanhMucs.FirstOrDefault(x => x.TenDanhMuc.ToLower().Equals(model.TenDanhMuc.ToLower()));
                if (item != null)
                {
                    return BadRequest(new { ErrorMessage = "Tên danh mục đã tồn tại" });
                }
                var newItem = new DanhMuc
                {
                    IdDanhMuc = Guid.NewGuid(),
                    MoTa = model.MoTa,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    TenDanhMuc = model.TenDanhMuc
                };
                _context.DanhMucs.Add(newItem);
                _context.SaveChanges();
                return Ok(new { Message = "Đã thêm danh mục mới" });
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi : {e.Message}" });
            }
        }
        [HttpPut("Categories/{IdDanhMuc:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult UpdateCategories(Guid IdDanhMuc,CategoriesRequestModel model)
        {
            try
            {
                var item = _context.DanhMucs.FirstOrDefault(x => x.IdDanhMuc==IdDanhMuc);
                if (item == null)
                {
                    return BadRequest(new { ErrorMessage = "Danh mục không tồn tại" });
                }
                else
                {
                    item.TenDanhMuc = model.TenDanhMuc;
                    item.MoTa = model.MoTa;
                    item.NgayCapNhat = DateTime.Now;
                    _context.DanhMucs.Update(item);
                    _context.SaveChanges();
                    return Ok(new { Message = "Đã cập nhật danh mục" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi : {e.Message}" });
            }
        }
        [HttpDelete("Categories/{IdDanhMuc:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult DeleteCategories(Guid IdDanhMuc)
        {
            try
            {
                var item = _context.DanhMucs.FirstOrDefault(x => x.IdDanhMuc==IdDanhMuc);
                if (item != null)
                {
                    return BadRequest(new { ErrorMessage = "Danh mục đã tồn tại" });
                }
                else
                {
                    _context.DanhMucs.Remove(item);
                    _context.SaveChanges();
                    return Ok(new { Message = "Đã thêm danh mục mới" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi : {e.Message}" });
            }
        }

        [HttpPost("Types/{IdDanhMuc:guid}")]
        [Authorize(Roles ="Administrator,Management")]
        public IActionResult AddTypes(Guid IdDanhMuc,TypesRequestModel model)
        {
            try
            {
                var danhmuc = _context.DanhMucs.FirstOrDefault(x => x.IdDanhMuc == IdDanhMuc);
                if (danhmuc == null)
                {
                    return NotFound(new { ErrorMessage = "Không tìm thấy danh mục nào" });
                }
                else
                {
                    var item = _context.Loais.FirstOrDefault(x => x.TenLoai.ToLower().Equals(model.TenLoai.ToLower()));
                    if (item != null)
                    {
                        return BadRequest(new { ErrorMessage = "Tên loại đã tồn tại" });
                    }
                    var newItem = new Loai
                    {
                        IdDanhMuc = IdDanhMuc,
                        MoTa = model.MoTa,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = DateTime.Now,
                        TenLoai = model.TenLoai,
                        IdLoai = Guid.NewGuid()

                    };
                    _context.Loais.Add(newItem);
                    _context.SaveChanges();
                    return Ok(new { Message = "Đã thêm loại mới" });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi : {e.Message}" });
            }
        }
        [HttpPut("Types/{IdLoai:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult UpdateType(Guid IdLoai,TypesRequestModel model)
        {
            try
            {
                var item = _context.Loais.FirstOrDefault(x => x.IdLoai==IdLoai);
                if (item == null)
                {
                    return BadRequest(new { ErrorMessage = "Loại không tồn tại" });
                }
                else
                {
                    item.MoTa = model.MoTa;
                    item.NgayCapNhat = DateTime.Now;
                    item.TenLoai = model.TenLoai;
                    _context.Loais.Update(item);
                    _context.SaveChanges();
                    return Ok(new { Message = "Đã cập nhật loại" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi : {e.Message}" });
            }
        }
        [HttpDelete("Types/{IdLoai:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult DeleteType(Guid IdLoai)
        {
            try
            {
                var item = _context.Loais.FirstOrDefault(x => x.IdLoai == IdLoai);
                if (item != null)
                {
                    return BadRequest(new { ErrorMessage = "Danh mục đã tồn tại" });
                }
                else
                {
                    _context.Loais.Remove(item);
                    _context.SaveChanges();
                    return Ok(new { Message = "Đã thêm danh mục mới" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi : {e.Message}" });
            }
        }


    }
}