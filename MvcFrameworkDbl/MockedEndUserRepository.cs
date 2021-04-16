using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure.Repository;

namespace MvcFrameworkDbl
{
    public class MockedEndUserRepository : IEndUserRepository
    {
        private readonly IMockedDataProvider _mockedDataProvider_;

        public MockedEndUserRepository(IMockedDataProvider mockedDataProvider)
        {
            _mockedDataProvider_ = mockedDataProvider;
        }


        public async Task<IEnumerable<EndUser>> GetAllEntitiesAsync()
        {
            return (await _mockedDataProvider_.GetMockedDataAsync()).Users;
        }

        public async Task<EndUser> GetEntityAsync(Int32 id, Boolean mustBeActive = true)
        {
            var user = (await _mockedDataProvider_.GetMockedDataAsync()).Users.FirstOrDefault(x => x.Id.Equals(id));
            if (user == null || (mustBeActive && !user.IsActive))
                throw new KeyNotFoundException($"User with id {id} not found");
            return user.ReturnWithoutSensitiveData();
        }

        public Task<Boolean> AddEntityAsync(EndUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<Boolean> UpdateEntityAsync(EndUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<Boolean> DeleteEntityAsync(Int32 id)
        {
            throw new NotImplementedException();
        }

        public async Task<Boolean> TryAuthenticateAsync(String email, String password)
        {
            return true;
        }

        public async Task<EndUser> GetUserWithSensitiveDataAsync(String email, Boolean mustBeActive = true)
        {
            var users = (await _mockedDataProvider_.GetMockedDataAsync()).Users;
            var user = users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (user == null || (mustBeActive && !user.IsActive))
                throw new KeyNotFoundException($"User with email {email} not found");
            return user;
        }

        public Task<Boolean> UpdateUserEmailAsync(Int32 id, String email)
        {
            throw new NotImplementedException();
        }

        public Task<Boolean> UpdateUserPasswordAsync(Int32 id, String password)
        {
            throw new NotImplementedException();
        }
    }
}