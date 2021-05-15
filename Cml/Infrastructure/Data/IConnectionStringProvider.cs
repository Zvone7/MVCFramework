using System;

namespace Cml.Infrastructure.Data
{
    public interface IConnectionStringProvider
    {
        String GetConnectionString();
    }
}