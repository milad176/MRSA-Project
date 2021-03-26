using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string passwortText);

        void Register(string userName, string password);

        Task<bool> UserAlreadyExist(string userName);
    }
}
