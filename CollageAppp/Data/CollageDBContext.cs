using CollageAppp.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace CollageAppp.Data
{
    public class CollageDBContext : DbContext
    {
        public CollageDBContext(DbContextOptions<CollageDBContext>options) : base(options)
        {
            
        }
        public DbSet<Student> Students {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.ApplyConfiguration(new StudentConfig());

        }
    }
}
