using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProfilerDatabase;
using System;
using System.Data.SqlClient;
using System.IO;
using Xunit;

namespace ProfilerTests
{
    public class Init
    {

        [Fact]
        public void InitAdminUser()
        {
            //IConfiguration configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())                       // Priority Order            overrides:
            //    .AddJsonFile("appsettings.json")                                    // add default properties.      ↑
            //    .AddEnvironmentVariables()                                          // add environment vaiables.    ↑
            //    .Build();

            //using (var connection = new SqlConnection(configuration["LocalDb"]))
            //{
            //    var salt = BCrypt.Net.BCrypt.GenerateSalt();
            //    var password = BCrypt.Net.BCrypt.HashPassword("admin", salt);
            //    var query = $"  INSERT INTO [dbo].[User] " +
            //         $"(Name, LastName, Email, Password, Salt, IsActive, DateJoined, Role, Token) " +
            //         $"VALUES('Admin', 'A', 'admin@mail.com', '{password}', '{salt}', 1, GETUTCDATE(), 'A', 'token'); ";
            //}
        }
    }
}
