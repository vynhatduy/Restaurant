using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NhaHang.Helpers;
using NhaHang.Models;
using NhaHang.Services;

namespace NhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly VnpaySettings _vnpaySettings;

        public PaymentController(IOptions<VnpaySettings> vnpaySettings)
        {
            _vnpaySettings = vnpaySettings.Value;
        }

        [HttpGet("VnPayReturn")]
        public IActionResult VnPayReturn()
        {
            string vnp_HashSecret = _vnpaySettings.vnp_HashSecret;
            VnPayLibrary vnpay = new VnPayLibrary();

            // Duyệt qua các query parameters của request
            foreach (var key in Request.Query.Keys)
            {
                vnpay.AddResponseData(key, Request.Query[key]);
            }

            string vnp_SecureHash = Request.Query["vnp_SecureHash"];
            if (vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret))
            {
                // Lấy các thông tin liên quan từ query
                string orderId = Request.Query["vnp_TxnRef"];
                string paymentStatus = Request.Query["vnp_TransactionStatus"];

                // Xử lý đơn hàng sau khi xác thực thành công
                if (paymentStatus == "00") // 00 là trạng thái giao dịch thành công
                {
                    // Cập nhật trạng thái đơn hàng ở đây
                    return Ok(new { Message = "Thanh toán thành công", OrderId = orderId });
                }
                else
                {
                    // Trường hợp giao dịch không thành công
                    return BadRequest(new { Message = "Giao dịch không thành công", OrderId = orderId });
                }
            }
            else
            {
                return BadRequest(new { Message = "Lỗi xác thực chữ ký" });
            }
        }

        [HttpPost("Pay")]
        public IActionResult Pay([FromBody] OrderInfo model)
        {
            string vnp_Returnurl = _vnpaySettings.vnp_Returnurl; // URL để nhận kết quả
            string vnp_Url = _vnpaySettings.vnp_Url; // URL thanh toán của VNPAY
            string vnp_TmnCode = _vnpaySettings.vnp_TmnCode; // Mã terminal code
            string vnp_HashSecret = _vnpaySettings.vnp_HashSecret; // Khóa bí mật

            // Tạo request VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0"); // Phiên bản API
            vnpay.AddRequestData("vnp_Command", "pay"); // Loại yêu cầu
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode); // Mã terminal code
            vnpay.AddRequestData("vnp_Amount", (model.ThanhTien * 100).ToString()); // Số tiền
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); // Thời gian tạo
            vnpay.AddRequestData("vnp_CurrCode", "VND"); // Đơn vị tiền tệ
            vnpay.AddRequestData("vnp_IpAddr", HttpContext.Connection.RemoteIpAddress.ToString()); // Địa chỉ IP
            vnpay.AddRequestData("vnp_Locale", "vn"); // Ngôn ngữ hiển thị
            vnpay.AddRequestData("vnp_OrderInfo", model.MoTa); // Thông tin đơn hàng
            vnpay.AddRequestData("vnp_OrderType", "other"); // Mã danh mục hàng hóa
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl); // URL nhận kết quả
            vnpay.AddRequestData("vnp_TxnRef", model.IdHoaDon.ToString()); // Mã tham chiếu giao dịch
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss")); // Thời gian hết hạn

            // Tạo URL thanh toán
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Trả về URL thanh toán để client chuyển hướng
            return Ok(new { paymentUrl });
        }
    }
}
