<<<<<<< HEAD
namespace GreenSale.Service.Security;

public class PasswordHasher
=======
﻿namespace GreenSale.Service.Security
>>>>>>> main
{
    public static (string Hash, string Salt) Hash(string password)
    {
<<<<<<< HEAD
        string salt = Guid.NewGuid().ToString();
        string hash = BCrypt.Net.BCrypt.HashPassword(password + salt);

        return (Hash: hash, Salt: salt);
    }
    public static bool Verify(string password, string salt, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password + salt, hash);
=======
        public static (string Hash, string Salt) Hash(string password)
        {
            string salt = Guid.NewGuid().ToString();
            string hash = BCrypt.Net.BCrypt.HashPassword(password + salt);

            return (Hash: hash, Salt: salt);
        }
        public static bool Verify(string password, string salt, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password + salt, hash);
        }
>>>>>>> main
    }
}
