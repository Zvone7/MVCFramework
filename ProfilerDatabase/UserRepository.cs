using ProfilerModels;
using ProfilerModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfilerDatabase
{
    public class UserRepository : IDataRepository<User>
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
            var user = _databaseContext.User.FirstOrDefault(x => x.Id == id);
            return user;
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
