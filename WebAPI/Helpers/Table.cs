namespace WebAPI.Helpers
{
    public class Table
    {
        public static bool UpdateTableStatus(Guid? IdBan, bool status,ApplicationDbContext context)
        {
            try
            {
                var item = context.Bans.FirstOrDefault(x => x.IdBan == IdBan);
                if (item == null)
                {
                    return false;
                }
                item.TrangThai = status;
                context.Bans.Update(item);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
