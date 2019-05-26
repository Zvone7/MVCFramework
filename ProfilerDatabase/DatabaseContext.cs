using Microsoft.EntityFrameworkCore;
using ProfilerModels;

namespace ProfilerDatabase
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<User> User { get; set; }
    }
}
