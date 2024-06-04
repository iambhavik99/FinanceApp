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
        public Task<UserResponseMedia> AddUser(UserRequestMedia userRequestMedia);

    }
}
