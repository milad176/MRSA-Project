using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebAPI.DbContexts;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> Authenticate(string userName, string passwortText)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
                return null;

            if (!MatchPasswordHash(passwortText, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        private bool MatchPasswordHash(string passwortText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwortText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }
        }

        public void Register(string userName, string password)
        {
            byte[] passwordKey, passwordHash;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var user = new User();
            user.Username = userName;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;

            _context.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExist(string userName)
        {
            return await _context.Users.AnyAsync(x => x.Username == userName);
        }
    }
}
