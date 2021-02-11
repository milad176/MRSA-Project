using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<User> Authenticate(string userName, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x=> x.Username == userName 
                 && x.Password == password);
        }
    }
}
