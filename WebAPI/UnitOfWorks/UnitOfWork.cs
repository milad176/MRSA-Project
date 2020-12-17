using System;
using System.Threading.Tasks;
using WebAPI.DbContexts;
using WebAPI.Interfaces.Repositories;
using WebAPI.Repositories;

namespace WebAPI.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ICityRepository CityRepository =>
            new CityRepository(_context);

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}