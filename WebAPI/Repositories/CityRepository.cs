using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.DbContexts;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCity(City city)
        {
            if(city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }
            _context.Cities.AddAsync(city);
        }

        public void DeleteCity(int cityId)
        {
            var city = _context.Cities.Find(cityId);
            _context.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetCity(int cityId)
        {
            return await _context.Cities.FindAsync(cityId);
        }
    }
}
