﻿using Dapper;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFrameworkDbl
{
    public class EndUserRepository : IEndUserRepository
    {
        private readonly IAppSettings appSettings;

        public EndUserRepository(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        #region SQL_HELPERS

        const string USER_TABLE = "[TestDb].[dbo].[User]";

        private String GetSelectUserSql(
            Int32 id = 0,
            String email = default,
            String password = default,
            Boolean mustBeActive = true)
        {
            var sql =
                $"SELECT " +
                $"[Id]" +
                $",[Name]" +
                $",[LastName]" +
                $",[Email]" +
                $",[Password]" +
                $",[Salt]" +
                $",[Role]" +
                $",[EmailConfirmed]" +
                $",[IsActive]" +
                $",[DateJoined]" +
                $"FROM {USER_TABLE}";
            if (id > 0)
            {
                sql += $" WHERE [Id] = {id}";
                if (mustBeActive)
                    sql += $" AND [IsActive] = 1";
            }
            else if (!String.IsNullOrWhiteSpace(email))
            {
                sql += $" WHERE [Email] like '{email}'";
                if (!String.IsNullOrWhiteSpace(password))
                    sql += $" AND [Password] like '{password}'";
                if (mustBeActive)
                    sql += $" AND [IsActive] = 1";
            }
            else
            {
                if (mustBeActive) sql += " WHERE [IsActive] = 1";
            }
            return sql;
        }

        private String GetInsertUserSql(EndUser user)
        {
            return
                $"INSERT INTO {USER_TABLE}" +
                $"([Name],[LastName]," +
                $"[Email],[Password]," +
                $"[Salt],[Role]," +
                $"[EmailConfirmed],[IsActive]," +
                $"[DateJoined])" +
                $"VALUES(" +
                $"'{user.Name}','{user.LastName}'," +
                $"'{user.Email}','{user.Password}'," +
                $"'{user.Salt}','{user.Role}'," +
                $"'{user.EmailConfirmed}','{user.IsActive}'," +
                $"'{user.DateJoined.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}')";
        }

        private String GetUpdateUserSql(EndUser user)
        {
            return
                $"UPDATE {USER_TABLE}" +
                $" SET" +
                $"[Name] = '{user.Name}'" +
                $",[LastName]= '{user.LastName}'" +
                //$",[Email]= '{user.Email}'" +
                //$",[Password]= '{user.Password}'" +
                //$",[Salt]= '{user.Salt}'" +
                //$",[Role]= '{user.Role}'" +
                //$",[EmailConfirmed]= '{user.EmailConfirmed}'" +
                //$",[IsActive]= '{user.IsActive}'" +
                //$",[DateJoined]= '{user.DateJoined.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                $" WHERE Id = {user.Id}";
        }

        private String GetUpdateUserEmailSql(Int32 id, String email)
        {
            return
                $"UPDATE {USER_TABLE}" +
                $" SET" +
                $"[Email]= '{email}'" +
                $" WHERE Id = {id}";
        }

        private String GetUpdateUserPasswordSql(Int32 id, String password)
        {
            return
                $"UPDATE {USER_TABLE}" +
                $" SET" +
                $"[Password]= '{password}'" +
                $" WHERE Id = {id}";
        }

        private String GetDeleteUserSql(Int32 id)
        {
            return
                $"UPDATE {USER_TABLE}" +
                $" SET" +
                $"[IsActive]= '{false}'" +
                $"WHERE Id = {id}";
        }

        #endregion

        public async Task<Boolean> AddEntityAsync(EndUser user)
        {
            var sql = $"{GetInsertUserSql(user)}";

            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.ExecuteAsync(sql);
                return result > 0;
            }
        }

        public async Task<Boolean> DeleteEntityAsync(Int32 id)
        {
            var sql = $"{GetDeleteUserSql(id)}";

            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.ExecuteAsync(sql);
                return result > 0 ? true : false;
            }
        }

        public async Task<EndUser> GetEntityAsync(Int32 id, Boolean mustBeActive = true)
        {
            var sql = $"{GetSelectUserSql(id)}";
            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<EndUser>(sql);
                var user = result.FirstOrDefault();
                if (user == null) throw new KeyNotFoundException($"User with id ({id}) not found.");
                if (result.Count() > 1) throw new InvalidProgramException($"Two users with same id ({id}) found.");
                if (!user.IsActive) throw new Exception($"User with id ({id}) not active.");
                if (!user.EmailConfirmed) throw new Exception($"User with email ({id}) not confirmed.");
                return user;
            }
        }

        /// <summary>
        /// Email must be hashed.
        /// </summary>
        public async Task<EndUser> GetUserWithSensitiveDataAsync(String email, Boolean mustBeActive = true)
        {
            var sql = $"{GetSelectUserSql(0, email, "", mustBeActive)}";
            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<EndUser>(sql);
                var user = result.FirstOrDefault();
                if (user == null) throw new KeyNotFoundException($"User with email {email} not found.");
                if (result.Count() > 1) throw new InvalidProgramException($"Two users with same email ({email} -> {user.Email}) found.");
                if (!user.IsActive) throw new Exception($"User with email ({email} -> {user.Email}) not active.");
                if (!user.EmailConfirmed) throw new Exception($"User with email ({email} -> {user.Email}) not confirmed.");
                return user;
            }
        }

        /// <summary>
        /// Email must be hashed.
        /// </summary>
        public async Task<Boolean> TryAuthenticateAsync(String email, String password)
        {
            var sql = $"{GetSelectUserSql(0, email, password)}";
            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<EndUser>(sql);
                var user = result.FirstOrDefault();
                if (user == null) throw new KeyNotFoundException($"User with email {email} and password {password} not found.");
                if (result.Count() > 1) throw new InvalidProgramException($"Two users with same email ({email} -> {user.Email}) found.");
                if (!user.IsActive) throw new Exception($"User with email ({email} -> {user.Email}) not active.");
                if (!user.EmailConfirmed) throw new Exception($"User with email ({email} -> {user.Email}) not confirmed.");
                return true;
            }
        }

        public async Task<IEnumerable<EndUser>> GetAllEntitiesAsync()
        {
            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<EndUser>(GetSelectUserSql());
                return result;
            }
        }

        public async Task<Boolean> UpdateEntityAsync(EndUser user)
        {
            var sql = $"{GetUpdateUserSql(user)}";

            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.ExecuteAsync(sql);
                return result > 0;
            }
        }

        public async Task<Boolean> UpdateUserEmailAsync(Int32 id, String email)
        {
            var sql = $"{GetUpdateUserEmailSql(id, email)}";

            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.ExecuteAsync(sql);
                return result > 0;
            }

        }

        public async Task<Boolean> UpdateUserPasswordAsync(Int32 id, String password)
        {
            var sql = $"{GetUpdateUserPasswordSql(id, password)}";

            using (var connection = new SqlConnection(appSettings.ConnectionString))
            {
                var result = await connection.ExecuteAsync(sql);
                return result > 0;
            }

        }
    }
}
