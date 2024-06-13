using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserResponseMedia> SignUp(UserRequestMedia userRequestMedia);
        public Task<Users> login(UserLoginRequestMedia userLoginRequestMedia);
        public Task<UserResponseMedia> GetUserInfo(Guid userId);
    }
}
