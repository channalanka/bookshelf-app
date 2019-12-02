using System.Linq;
using System.Threading.Tasks;
using bookshelf_api.Auth;
using bookshelf_api.Common;
using bookshelf_api.Models;
using Microsoft.EntityFrameworkCore;

namespace bookshelf_api.Repository
{
    public class UserRepo : IUser
    {
        BookshelfContext db;
        IAuthSecurity authSecurity;

        public UserRepo(BookshelfContext db, IAuthSecurity authSecurity)
        {
            this.db = db;
            this.authSecurity = authSecurity;
        }


        public async Task<int> CreateUser(User user)
        {
            if (this.db.Users.FirstOrDefault(x => x.UserName == user.UserName) != null)
                throw new ApiValidationException("User Already exsist with given username");

            user.Password = this.authSecurity.GenerateHash(user.Password);
            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User> GetUserByUserName(string username, string password)
        {
            password = this.authSecurity.GenerateHash(password);
            return await this.db.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }

    }
}
