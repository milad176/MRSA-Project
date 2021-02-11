using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
    }
}
