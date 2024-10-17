namespace WebAPI.Helpers
{
    public class Table
    {
        public static bool UpdateTableStatus(Guid? IdBan, bool status,string apiUrl,string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{apiUrl}/api/Tables/Table/UpdateStatus/{IdBan}?status={status}";
                    var response = client.PutAsync(url, null).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Error Content: {errorContent}");
                    }
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                return false;
            }
        }
    }
}
