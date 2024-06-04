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
        public async Task<UserResponseMedia> AddUser(UserRequestMedia userRequestMedia)
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
    }
}
