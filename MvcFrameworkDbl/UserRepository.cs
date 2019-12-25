using Microsoft.EntityFrameworkCore;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFrameworkDbl
{
    public class UserRepository : IUserRepository
    {
        readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;

        public UserRepository(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        public async Task AddAsync(EndUser user)
        {
            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                await context.User.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Int32 id)
        {
            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                var user = await context.User.FindAsync(id);
                user.IsActive = false;
                context.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<EndUser> GetAsync(Int32 id)
        {
            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                var user = await context.User.FindAsync(id);
                return user;
            }
        }

        public async Task<EndUser> GetAsync(String email)
        {
            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                var user = context.User.FirstOrDefault(x => x.Email.Equals(email));
                return user;
            }
        }

        public async Task<Boolean> TryAuthenticateAsync(String email, String password)
        {
            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                var user = context.User.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
                if (user != null) return true; else return false;
            }
        }

        public async Task<IEnumerable<EndUser>> GetAllAsync()
        {
            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                var users = context.User.Select(x => x).Where(u => u.IsActive);
                return users;
            }
        }

        public async Task UpdateAsync(EndUser entity)
        {
            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                context.Update(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
