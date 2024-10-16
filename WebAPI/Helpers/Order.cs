using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPI.Models.Request;

namespace WebAPI.Helpers
{
    public class Order
    {
        public static bool AddOrder(Guid IdHoaDon, Guid IdNhanVien, OrderRequestModel model, ApplicationDbContext context)
        {
            try
            {

                decimal? thanhTien = 0;
                foreach (var item in model.DsChiTietHoaDon)
                {
                    thanhTien += item.SoLuong * item.GiaBan;
                }
                var newItem = new HoaDon
                {
                    IdHoaDon = IdHoaDon,
                    IdBan = model.IdBan,
                    ChietKhau = "5%",
                    Coupon = model.Coupon,
                    DaThanhToan = model.DaThanhToan,
                    IdNhanVien = IdNhanVien,
                    IdKhachHang = model.IdKhachHang,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    PhuongThucThanhToan = "null",
                    ThanhTien = thanhTien,
                    TongHoaDon = thanhTien + thanhTien * 5 / 100
                };

                context.HoaDons.Add(newItem);
                context.SaveChanges();


                var addDetail = AddDetailOrder(IdHoaDon, model.DsChiTietHoaDon, context);
                if (addDetail)
                {
                    return true;
                }
                else
                {
                    context.HoaDons.Where(x => x.IdHoaDon == IdHoaDon).ExecuteDelete();
                    context.SaveChanges();
                    Console.WriteLine("Không thể thêm orderdetail vào db và order đã bị xóa");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi tại order :{e.Message}");
                return false;
            }
        }
        public static bool AddDetailOrder(Guid IdHoaDon, List<OrderDetailRequestModel> model, ApplicationDbContext context)
        {
            try
            {
                
                foreach (var item in model)
                {
                    context.ChiTietHoaDons.Add(new ChiTietHoaDon
                    {
                        IdChiTietHoaDon = Guid.NewGuid(),
                        IdHoaDon = IdHoaDon,
                        IdSanPham = item.IdSanPham,
                        GiaBan = item.GiaBan,
                        SoLuong = item.SoLuong,
                        ThanhTien = item.SoLuong * item.GiaBan,
                        TrangThai = item.TrangThai
                    });
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi tại orderdetail :{e.Message}");// Log lỗi nếu cần thiết
                return false;
            }
        }
        public static void LogOrder(OrderRequestModel model)
        {
            // Hiển thị thông tin yêu cầu lên console
            Console.WriteLine("Tạo đơn hàng với thông tin: ");
            Console.WriteLine($"Id Ban: {model.IdBan}");
            Console.WriteLine($"Id Khách Hàng: {model.IdKhachHang}");
            Console.WriteLine($"Chiết Khấu: {model.ChietKhau}");
            Console.WriteLine($"Coupon: {model.Coupon}");
            Console.WriteLine($"Đã Thanh Toán: {model.DaThanhToan}");
            Console.WriteLine($"Phương Thức Thanh Toán: {model.PhuongThucThanhToan}");

            LogOrderDetail(model.DsChiTietHoaDon);
        }
        public static void LogOrderDetail(List<OrderDetailRequestModel> ds)
        {
            // Hiển thị chi tiết hóa đơn
            foreach (var item in ds)
            {
                Console.WriteLine($"Sản Phẩm: {item.IdSanPham}, Số Lượng: {item.SoLuong}, Giá Bán: {item.GiaBan}, Trạng Thái: {item.TrangThai}");
            }
        }
    }
}
