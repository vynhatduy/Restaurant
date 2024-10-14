using System;
using System.Security.Cryptography;

public class HasherPassword
{
    public static (string Hash, string Salt) HasherPass(string password)
    {
        try
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Mật khẩu không được để trống");
            }

            byte[] saltBytes = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(64); 

                string salt = Convert.ToBase64String(saltBytes);
                string hash = Convert.ToBase64String(hashBytes);

                return (Hash: hash, Salt: salt);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return (Hash: null, Salt: null);
        }
    }
    public static bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        using (var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, saltBytes, 10000))
        {
            byte[] hashBytes = pbkdf2.GetBytes(64);

            string hashToVerify = Convert.ToBase64String(hashBytes);
            return hashToVerify == storedHash;
        }
    }
}
