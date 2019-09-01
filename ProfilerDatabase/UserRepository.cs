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
        readonly DatabaseContext _databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task Add(User user)
        {
            try
            {
                _databaseContext.User.Add(user);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }
        public async Task Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(Int32 id)
        {
            try
            {
                var user = _databaseContext.User.FirstOrDefault(x => x.Id == id);
                return user;
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }
        public User Get(String email)
        {
            try
            {
                var user = _databaseContext.User.FirstOrDefault(x => x.Email == email);
                return user;
            }
            catch (Exception)
            {
                //todo logging
                throw;
            }
        }

        public Boolean TryAuthenticate(String email, String password)
        {
            try
            {
                var user = _databaseContext.User.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (user != null) return true; else return false;
            }
            catch (Exception e)
            {
                //todo logging
                throw;
            }
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(User dbEntity, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
