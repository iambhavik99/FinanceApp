using FinanceApp.Infrastructure.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseMedia> SignUp(UserRequestMedia userRequestMedia);
        public Task<UserResponseMedia> login(UserLoginRequestMedia userLoginRequestMedia, string aesKeyString, string ivString);
        public bool authenticate(string password, string hashPassword, string aesKeyString, string ivString);
        public Task<UserResponseMedia> GetUserInfo(Guid userId);

    }
}
