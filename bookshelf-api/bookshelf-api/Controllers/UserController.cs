using System.Threading.Tasks;
using bookshelf_api.Common;
using bookshelf_api.Models;
using bookshelf_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace bookshelf_api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUser userRepo;

        public UserController(IUser userRepo)
        {
            this.userRepo = userRepo;
        }


        /// <summary>
        /// New user Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> CreateUser([FromBody] User user)
        {
            if (user == null)
                throw new ApiValidationException("User Data is null");

            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.ConfirmPassword))
                throw new ApiValidationException("Password and confirm password is required");

            if (!string.Equals(user.Password, user.ConfirmPassword))
                throw new ApiValidationException("Password and confirm password should be same");

            if (string.IsNullOrEmpty(user.UserName))
                throw new ApiValidationException("Username is required");

            var userModel = new User();
            userModel.UserName = user.UserName;
            userModel.Name = user.Name;
            userModel.Password = user.Password;

            return await this.userRepo.CreateUser(userModel);
        }
    }
}
