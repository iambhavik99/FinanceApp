using FinanceApp.Application.Interfaces;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseMedia> AddUser(UserRequestMedia userRequestMedia)
        {
            return await _userRepository.AddUser(userRequestMedia);
        }
    }
}
