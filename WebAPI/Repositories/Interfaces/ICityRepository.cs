using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City> GetCity(int cityId);

        void AddCity(City city);

        void DeleteCity(int cityId);

    }
}
