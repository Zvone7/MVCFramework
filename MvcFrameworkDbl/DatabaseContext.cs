using Microsoft.EntityFrameworkCore;
using MvcFrameworkCml;

namespace MvcFrameworkDbl
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<EndUser> User { get; set; }
    }
}
