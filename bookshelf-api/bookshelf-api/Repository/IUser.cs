using System.Threading.Tasks;
using bookshelf_api.Models;

namespace bookshelf_api.Repository
{
    public interface IUser
    {
        Task<User> GetUserByUserName(string username, string password);
        Task<int> CreateUser(User user);
    }
}
