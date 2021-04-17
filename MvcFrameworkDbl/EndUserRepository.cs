using Dapper;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MvcFrameworkCml.DataModels;
using MvcFrameworkCml.Infrastructure.Data;

namespace MvcFrameworkDbl
{
    public class EndUserRepository : IEndUserRepository
    {
        private readonly String _connectionString_;

        public EndUserRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString_ = connectionStringProvider.GetConnectionString();
        }

        public async Task<Boolean> AddEntityAsync(EndUser user)
        {
            var sql = $@"INSERT INTO [User]
                            ([Name],[LastName],[Email],
                            [Password],[Salt],[Role],
                            [EmailConfirmed],[IsActive],[DateJoined])
                        VALUES(
                            @name,@lastName,@email,
                            @password,@salt,@role,
                            @emailConfirmed,@isActive,@dateJoined)";

            await using var connection = new SqlConnection(_connectionString_);
            var result = await connection.ExecuteAsync(sql, new
            {
                name = user.Name,
                lastName = user.LastName,
                email = user.Email,
                password = user.Password,
                salt = user.Salt,
                role = user.Role,
                emailConfirmed = user.EmailConfirmed,
                isActive = user.IsActive,
                dateJoined = user.DateJoined.ToString("yyyy-MM-dd HH:mm:ss.fff")
            });
            return result > 0;
        }

        public async Task<Boolean> DeleteEntityAsync(Int32 id)
        {
            var sql =
                $@"  UPDATE [User]
                     SET [IsActive]= false
                     WHERE Id = @id";
            await using var connection = new SqlConnection(_connectionString_);
            var result = await connection.ExecuteAsync(sql, new {id = id});
            return result > 0 ? true : false;
        }

        public async Task<EndUser> GetEntityAsync(Int32 id, Boolean mustBeActive = true)
        {
            var sql = $@"SELECT TOP 1
                            [Id],[Name],[LastName],
                            [Email],[Password],[Salt],
                            [Role],[EmailConfirmed],
                            [IsActive],[DateJoined]
                        FROM [User] 
                        WHERE Id = @id";
            await using var connection = new SqlConnection(_connectionString_);
            var result = (await connection.QueryAsync<EndUser>(sql, new {id = id})).ToList();
            var user = result.Single();
            if (mustBeActive && !user.IsActive) throw new Exception($"User with id ({id}) not active.");
            if (!user.EmailConfirmed) throw new Exception($"User with email ({id}) not confirmed.");
            return user;
        }

        /// <summary>
        /// Email must be hashed.
        /// </summary>
        public async Task<EndUser> GetUserWithSensitiveDataAsync(String email, Boolean mustBeActive = true)
        {
            var sql = $@"SELECT TOP 1
                            [Id],[Name],[LastName],
                            [Email],[Password],[Salt],
                            [Role],[EmailConfirmed],
                            [IsActive],[DateJoined]
                        FROM [User] 
                        WHERE Email = @email";
            await using var connection = new SqlConnection(_connectionString_);
            var result = (await connection.QueryAsync<EndUser>(sql, new {email = email})).ToList();
            var user = result.Single();
            if (user == null) throw new KeyNotFoundException($"User with email {email} not found.");
            if (mustBeActive && !user.IsActive) throw new Exception($"User with email ({email}) not active.");
            if (!user.EmailConfirmed) throw new Exception($"User with email ({email} -> {user.Email}) not confirmed.");
            return user;
        }

        /// <summary>
        /// Email must be hashed.
        /// </summary>
        public async Task<Boolean> TryAuthenticateAsync(String emailEncrypted, String passwordEncrypted)
        {
            var sql = $@"SELECT TOP 1
                            [Id],[Name],[LastName],
                            [Email],[Password],[Salt],
                            [Role],[EmailConfirmed],
                            [IsActive],[DateJoined]
                        FROM [User] 
                        WHERE Email = @email
                        AND Password = @password";
            await using var connection = new SqlConnection(_connectionString_);
            var result = (await connection.QueryAsync<EndUser>(sql, new
            {
                email = emailEncrypted,
                password = passwordEncrypted
            })).ToList();
            var user = result.Single();
            if (user == null) throw new KeyNotFoundException($"User with email/password combination not found.");
            if (result.Count > 1) throw new InvalidProgramException($"Two users with same  email/password combination found.");
            if (!user.IsActive) throw new Exception($"User with  email/password combination not active.");
            if (!user.EmailConfirmed) throw new Exception($"User with  email/password combination not confirmed.");
            return true;
        }

        public async Task<IEnumerable<EndUser>> GetAllEntitiesAsync()
        {
            var sql = $@"SELECT
                            [Id],[Name],[LastName],
                            [Email],[Password],[Salt],
                            [Role],[EmailConfirmed],
                            [IsActive],[DateJoined]
                        FROM [User]";
            await using var connection = new SqlConnection(_connectionString_);
            var result = await connection.QueryAsync<EndUser>(sql);
            return result;
        }

        public async Task<Boolean> UpdateEntityAsync(EndUser user)
        {
            var sql =
                $@"UPDATE [User]
                   SET
                    [Name] = @name,
                    [LastName]= @lastName
                   WHERE Id = @id";

            await using var connection = new SqlConnection(_connectionString_);
            var result = await connection.ExecuteAsync(sql,
                new
                {
                    id = user.Id,
                    name = user.Name,
                    lastName = user.LastName
                });
            return result > 0;
        }

        public async Task<Boolean> UpdateUserEmailAsync(Int32 id, String emailEncrypted)
        {
            var sql =
                $@"  UPDATE [User]
                     SET [Email]= @email
                     WHERE Id = @id";
            await using var connection = new SqlConnection(_connectionString_);
            var result = await connection.ExecuteAsync(sql,
                new
                {
                    id = id,
                    email = emailEncrypted
                });
            return result > 0;
        }

        public async Task<Boolean> UpdateUserPasswordAsync(Int32 id, String passwordHashed)
        {
            var sql =
                $@"  UPDATE [User]
                     SET [Password]= @password
                     WHERE Id = @id";
            await using var connection = new SqlConnection(_connectionString_);
            var result = await connection.ExecuteAsync(sql,
                new
                {
                    id = id,
                    password = passwordHashed
                });
            return result > 0;
        }
    }
}