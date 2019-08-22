using ProfilerModels;
using ProfilerModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfilerDatabase
{
    public class UserRepository : IUserRepository
    {
        readonly DatabaseContext _databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(User entity)
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
        public User Get(String username)
        {
            try
            {
                var user = _databaseContext.User.FirstOrDefault(x => x.Username == username);
                return user;
            }
            catch (Exception)
            {
                //todo logging
                throw;
            }
        }

        public Boolean TryAuthenticate(String username, String password)
        {
            try
            {
                var user = _databaseContext.User.FirstOrDefault(x => x.Username == username && x.Password == password);
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

        public void Update(User dbEntity, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
