using Microsoft.EntityFrameworkCore;
using ProfilerModels;
using ProfilerModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfilerDatabase
{
    public class UserRepository : IUserRepository
    {
        readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        public UserRepository(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }
        public async Task Add(EndUser user)
        {
            try
            {
                using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
                {
                    context.User.Add(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }
        public async Task Delete(EndUser entity)
        {
            throw new NotImplementedException();
        }

        public EndUser Get(Int32 id)
        {
            try
            {
                using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
                {
                    var user = context.User.FirstOrDefault(x => x.Id == id);
                    return user;
                }
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }
        public EndUser Get(String email)
        {
            try
            {
                using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
                {
                    var user = context.User.FirstOrDefault(x => x.Email.Equals(email));
                    return user;
                }
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }

        public Boolean TryAuthenticate(String email, String password)
        {
            try
            {
                using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
                {
                    var user = context.User.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
                    if (user != null) return true; else return false;
                }
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }

        public IEnumerable<EndUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(EndUser entity)
        {
            try
            {
                using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
                {
                    context.Update(entity);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }
    }
}
