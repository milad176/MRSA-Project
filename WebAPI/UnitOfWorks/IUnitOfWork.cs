using System.Threading.Tasks;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.UnitOfWorks
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }

        IUserRepository UserRepository { get; }

        Task<bool> CompleteAsync();
    }
}
