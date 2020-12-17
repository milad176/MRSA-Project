using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces.Repositories;

namespace WebAPI.UnitOfWorks
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }

        Task<bool> CompleteAsync();
    }
}
