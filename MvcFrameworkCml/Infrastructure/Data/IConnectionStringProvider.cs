using System;

namespace MvcFrameworkCml.Infrastructure.Data
{
    public interface IConnectionStringProvider
    {
        String GetConnectionString();
    }
}