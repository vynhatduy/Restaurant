using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models.Response;
using WebAPI.Helpers;
using WebAPI.Models.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController:ControllerBase
    {
        private ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Order")]
        public IActionResult GetAllOrder()
        {
            try
            {
                return Ok(_context.HoaDons.Select(x => new OrderResponseModel
                {
                    IdHoaDon = x.IdHoaDon,
                    IdNhanVien = x.IdNhanVien,
                    SoBan = x.Ban.SoBan,
                    NgayTao = x.NgayTao,
                    TrangThai = x.TrangThai,
                    DaThanhToan = x.DaThanhToan,
                    TongTien = x.ThanhTien
                }).ToList());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("OrderDetail")]
        public IActionResult GetAllOrderDetail()
        {
            try
            {
                return Ok(_context.HoaDons.Select(x=>new OrderDetailResponseModel
                {
                    IdHoaDon=x.IdHoaDon,
                    IdNhanVien=x.IdNhanVien,
                    TongTien=x.ThanhTien,
                    SoBan=x.Ban.SoBan,
                    DsSanPham=_context.ChiTietHoaDons.Where(x=>x.IdHoaDon==x.IdHoaDon).Select(sp=>new ProductResponseModel
                    {
                        IdSanPham=sp.IdSanPham,
                        DonGia=sp.GiaBan,
                        HinhAnh=sp.SanPham.HinhAnh,
                        Loai=sp.SanPham.Loai.TenLoai,
                        MoTa=sp.SanPham.MoTa,
                        SoLuong=sp.SoLuong,
                        TrangThai=sp.TrangThai,
                        SoSao=sp.SanPham.SoSao
                    }).ToList()
                }).ToList());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("OrderByStatus/{TrangThai:bool}")]

        public IActionResult GetAllOrderByStatus(bool TrangThai)
        {
            try
            {
                return Ok(_context.HoaDons.Where(x=>x.TrangThai==TrangThai).Select(x => new OrderResponseModel
                {
                    IdHoaDon = x.IdHoaDon,
                    IdNhanVien = x.IdNhanVien,
                    SoBan = x.Ban.SoBan,
                    NgayTao = x.NgayTao,
                    TrangThai = x.TrangThai,
                    DaThanhToan = x.DaThanhToan,
                    TongTien = x.ThanhTien
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("OrderDetailByStatus/{TrangThai:bool}")]
        public IActionResult GetAllOrderDetailByStatus(bool TrangThai)
        {
            try
            {
                return Ok(_context.HoaDons.Select(x => new OrderDetailResponseModel
                {
                    IdHoaDon = x.IdHoaDon,
                    IdNhanVien = x.IdNhanVien,
                    TongTien = x.ThanhTien,
                    SoBan = x.Ban.SoBan,
                    DsSanPham = _context.ChiTietHoaDons.Where(x => x.IdHoaDon == x.IdHoaDon&&x.TrangThai==TrangThai).Select(sp => new ProductResponseModel
                    {
                        IdSanPham = sp.IdSanPham,
                        DonGia = sp.GiaBan,
                        HinhAnh = sp.SanPham.HinhAnh,
                        Loai = sp.SanPham.Loai.TenLoai,
                        MoTa = sp.SanPham.MoTa,
                        SoLuong = sp.SoLuong,
                        TrangThai = sp.TrangThai,
                        SoSao = sp.SanPham.SoSao
                    }).ToList()
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("OrderDetailByOrderId/{IdHoaDon:guid}")]
        public IActionResult GetOrderDetailByOrderId(Guid IdHoaDon)
        {
            try
            {
                return Ok(_context.HoaDons.Where(x=>x.IdHoaDon==IdHoaDon).Select(x => new OrderDetailResponseModel
                {
                    IdHoaDon = x.IdHoaDon,
                    IdNhanVien = x.IdNhanVien,
                    TongTien = x.ThanhTien,
                    SoBan = x.Ban.SoBan,
                    DsSanPham = _context.ChiTietHoaDons.Where(x => x.IdHoaDon == x.IdHoaDon).Select(sp => new ProductResponseModel
                    {
                        IdSanPham = sp.IdSanPham,
                        DonGia = sp.GiaBan,
                        HinhAnh = sp.SanPham.HinhAnh,
                        Loai = sp.SanPham.Loai.TenLoai,
                        MoTa = sp.SanPham.MoTa,
                        SoLuong = sp.SoLuong,
                        TrangThai = sp.TrangThai,
                        SoSao = sp.SanPham.SoSao
                    }).ToList()
                }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Order/Create/{IdNhanVien:guid}")]
        public IActionResult CreateOrderByEmployee(Guid IdNhanVien, OrderRequestModel model)
        {
            try
            {
                var item = _context.HoaDons.Where(x => x.IdBan == model.IdBan).OrderByDescending(x => x.NgayCapNhat).FirstOrDefault();
                if (item != null)
                {
                    if(item.TrangThai == true && item.DaThanhToan == true)
                    {
                        var newGuid = Guid.NewGuid();
                        var addOrder =Order.AddOrder(newGuid, IdNhanVien, model, _context);
                        if (addOrder)
                        {
                            return Ok(new { Message = $"Tạo đơn hàng mới thành công với Id là : {newGuid}" });
                        }
                        return BadRequest(new { ErrorMessage = $"Xảy ra lỗi không xác định" });
                    }
                    else
                    {
                        return BadRequest(new { ErrorMessage = $"Bàn hiện tại không khả dụng" });
                    }
                }
                else
                {
                    var newGuid = Guid.NewGuid();
                    var addOrder = Order.AddOrder(newGuid, IdNhanVien, model, _context);
                    if (addOrder)
                    {
                        return Ok(new { Message = $"Tạo đơn hàng mới thành công với Id là : {newGuid}" });
                    }
                    return BadRequest(new { ErrorMessage = $"Xảy ra lỗi không xác định" });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
       
    }
}
