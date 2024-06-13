using FinanceApp.Domain.DBContext;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<UserResponseMedia> SignUp(UserRequestMedia userRequestMedia)
        {
            Users users = new Users();

            // set request media to data model
            users.id = Guid.NewGuid();
            users.password = userRequestMedia.password;
            users.email = userRequestMedia.email;
            users.username = userRequestMedia.username;
            users.createdAt = DateTime.UtcNow;

            // save data to table
            await _context.Users.AddAsync(users);
            await _context.SaveChangesAsync();

            var response = await _context.Users.FirstOrDefaultAsync(x => x.id == users.id);
            if (response != null)
            {
                return new UserResponseMedia()
                {
                    email = response.email,
                    username = response.username,
                    id = response.id
                };
            }
            return null;
        }

        public async Task<Users> login(UserLoginRequestMedia userLoginRequestMedia)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.username == userLoginRequestMedia.username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user;
        }

        public async Task<UserResponseMedia> GetUserInfo(Guid userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.id == userId);
                
                UserResponseMedia userResponseMedia = new UserResponseMedia();
                if (user != null) { 
                    
                    userResponseMedia.id = userId;
                    userResponseMedia.username = user.username;
                    userResponseMedia.email = user.email;
                }

                return userResponseMedia;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
