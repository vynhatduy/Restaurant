using NhaHang.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace NhaHang.Services
{
    public class VnPayLibrary
    {
        private SortedList<string, string> requestData = new SortedList<string, string>();

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                requestData.Add(key, value);
            }
        }
        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                requestData[key] = value;
            }
        }
        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            string queryString = "";
            foreach (KeyValuePair<string, string> kvp in requestData)
            {
                queryString += WebUtility.UrlEncode(kvp.Key) + "=" + WebUtility.UrlEncode(kvp.Value) + "&";
            }

            string signData = string.Join("&", requestData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            string secureHash = HmacSHA512(vnp_HashSecret, signData);
            return baseUrl + "?" + signData + "&vnp_SecureHash=" + secureHash;
        }
        private string HmacSHA512(string key, string input)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }
        public bool ValidateSignature(string inputHash, string secretKey)
        {
            // Tạo chuỗi raw data từ dữ liệu phản hồi
            string rawData = string.Join("&", requestData
                .Where(kv => !kv.Key.StartsWith("vnp_SecureHash"))
                .Select(kv => $"{kv.Key}={kv.Value}"));

            // Tạo checksum từ raw data
            string myChecksum = HmacSHA512(secretKey, rawData);

            // So sánh checksum
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
    
}
