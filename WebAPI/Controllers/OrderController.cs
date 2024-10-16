using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrator,Management,User")]
        public IActionResult CreateOrderByEmployee(Guid IdNhanVien, OrderRequestModel model)
        {
            try
            {
                var nhanvien = _context.NhanViens.FirstOrDefault(x => x.IdNhanVien == IdNhanVien);
                if (nhanvien != null)
                {
                    var item = _context.HoaDons.Where(x => x.IdBan == model.IdBan).OrderByDescending(x => x.NgayCapNhat).FirstOrDefault();
                    if (item != null)
                    {
                        if (item.TrangThai == true && item.DaThanhToan == true)
                        {
                            var newGuid = Guid.NewGuid();
                            var addOrder = Order.AddOrder(newGuid, IdNhanVien, model, _context);
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
                return BadRequest(new { Message = "Không có nhân viên nào như trên" });
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpPost("Order/Create")]
        public IActionResult CreateOrderByCustomer(OrderRequestModel model)
        {
            try
            {
                var item = _context.HoaDons.Where(x => x.IdBan == model.IdBan).OrderByDescending(x => x.NgayCapNhat).FirstOrDefault();
                if (item != null)
                {
                    if (item.TrangThai == true && item.DaThanhToan == true)
                    {
                        var newGuid = Guid.NewGuid();
                        var addOrder = Order.AddOrder(IdHoaDon:newGuid, IdNhanVien:Guid.Empty ,model:model,context:_context);
                        if (addOrder)
                        {
                            return Ok(new { Message = $"Đơn hàng của bạn đã được tạo vui lòng chờ nhân viên xác nhận : {newGuid}" });
                        }
                        return BadRequest(new { ErrorMessage = $"Xảy ra lỗi không xác định vui lòng liên hệ nhân viên" });
                    }
                    else
                    {
                        return BadRequest(new { ErrorMessage = $"Bàn hiện tại không khả dụng" });
                    }
                }
                else
                {
                    var newGuid = Guid.NewGuid();
                    var addOrder = Order.AddOrder(IdHoaDon: newGuid, IdNhanVien: Guid.Empty, model: model, context: _context);
                    if (addOrder)
                    {
                        return Ok(new { Message = $"Đơn hàng của bạn đã được tạo vui lòng chờ nhân viên xác nhận : {newGuid}" });
                    }
                    return BadRequest(new { ErrorMessage = $"Xảy ra lỗi không xác định vui lòng liên hệ nhân viên" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpPut("Order/Update/{IdBan:guid}/{IdNhanVien:guid}")]
        [Authorize(Roles ="Administrator,Management,User")]
        public IActionResult UpdateOrder(Guid IdBan,Guid IdNhanVien)
        {
            try
            {
                var nhanvien = _context.NhanViens.FirstOrDefault(x => x.IdNhanVien == IdNhanVien);
                if(nhanvien != null)
                {
                    var item = _context.HoaDons.Where(x => x.IdBan == IdBan).OrderByDescending(x => x.NgayCapNhat).FirstOrDefault();
                    if (item != null)
                    {
                        if (item.TrangThai == false)
                        {
                            item.TrangThai = true;
                            item.IdNhanVien = IdNhanVien;
                            item.NgayCapNhat = DateTime.Now;
                            _context.HoaDons.Update(item);
                            _context.SaveChanges();
                            return Ok($"Nhân viên: {nhanvien.HoTen} đã cập nhật đơn hàng");
                        }
                        else
                        {
                            return NotFound(new { Message = "Đơn hàng mới nhất đã được cập nhật" });
                        }
                    }
                    return NotFound(new { Message = "Bàn này chưa có hóa đơn nào" });
                }
                else
                {
                    return BadRequest(new { Message = "Không có nhân viên nào như trên" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpPut("OrderDetail/Update/{IdHoaDon:guid}/{IdNhanVien:guid}")]
        [Authorize(Roles = "Administrator,Management,User")]
        public IActionResult UpdateOrderDetail(Guid IdHoaDon, Guid IdNhanVien, List<Guid> DsIdSanPham)
        {
            try
            {
                var nhanvien = _context.NhanViens.FirstOrDefault(x => x.IdNhanVien == IdNhanVien);
                if (nhanvien == null)
                {
                    return BadRequest(new { Message = "Không có nhân viên nào như trên" });
                }

                var hoaDon = _context.HoaDons.FirstOrDefault(x => x.IdHoaDon == IdHoaDon);
                if (hoaDon == null)
                {
                    return NotFound(new { Message = "Hóa đơn không tồn tại" });
                }

                if (hoaDon.TrangThai == false)
                {
                    return BadRequest(new { Message = "Đơn hàng đang chờ xác nhận" });
                }

                if (DsIdSanPham.Count > 0)
                {
                    var chiTietHoaDons = _context.ChiTietHoaDons
                    .Where(cthd => cthd.IdHoaDon == IdHoaDon && DsIdSanPham.Contains(cthd.IdSanPham.Value))
                    .ToList();

                    foreach (var chiTiet in chiTietHoaDons)
                    {
                        if (chiTiet.TrangThai == false)
                        {
                            chiTiet.TrangThai = true;
                        }
                    }

                    _context.SaveChanges();

                    return Ok(new { Message = "Cập nhật trạng thái thành công" });
                }
                else
                {
                    return BadRequest(new { Message = "Khong có id sản phẩm nào được truyền nvaof" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }

        [HttpDelete("Order/Delete/{IdHoaDon:guid}/{IdNhanVien:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult DeleteOrder(Guid IdHoaDon, Guid IdNhanVien)
        {
            try
            {
                var nhanvien = _context.NhanViens.FirstOrDefault(x => x.IdNhanVien == IdNhanVien);
                if (nhanvien != null)
                {
                    var item = _context.HoaDons.Where(x => x.IdHoaDon == IdHoaDon).FirstOrDefault();
                    if (item != null)
                    {
                        var result = Order.DeleteOrder(IdHoaDon: IdHoaDon, context: _context);
                        if (result == true)
                        {
                            return Ok(new { Message = $"Nhân viên {nhanvien.HoTen} đã xóa thành công đơn hàng {IdHoaDon}" });
                        }
                        return BadRequest(new {Message= "Xóa không thành công vui lòng kiểm tra lại log"});
                    }
                    return NotFound(new { Message = "Không tìm thấy hóa đơn yêu cầu" });
                }
                else
                {
                    return BadRequest(new { Message = "Không có nhân viên nào như trên" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpDelete("OrderDetail/Delete/{IdHoaDon:guid}/{IdNhanVien:guid}/{IdSanPham:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult DeleteOrderDetail(Guid IdHoaDon, Guid IdNhanVien,Guid IdSanPham)
        {
            try
            {
                var nhanvien = _context.NhanViens.FirstOrDefault(x => x.IdNhanVien == IdNhanVien);
                if (nhanvien != null)
                {
                    var item = _context.HoaDons.Where(x => x.IdHoaDon == IdHoaDon).FirstOrDefault();
                    if (item != null)
                    {
                        var result = Order.DeleteOrderDetail(IdHoaDon: IdHoaDon,IdSanPham:IdSanPham, context: _context);
                        if (result == true)
                        {
                            return Ok(new { Message = $"Nhân viên {nhanvien.HoTen} đã xóa thành công sản phẩm {IdSanPham} trong chi tiết đơn hàng {IdHoaDon}" });
                        }
                        return BadRequest(new {Message= "Xóa không thành công vui lòng kiểm tra lại log"});
                    }
                    return NotFound(new { Message = "Bàn này chưa có hóa đơn nào" });
                }
                else
                {
                    return BadRequest(new { Message = "Không có nhân viên nào như trên" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
    }
}
