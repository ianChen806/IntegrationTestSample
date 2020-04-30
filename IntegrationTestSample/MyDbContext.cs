using Microsoft.EntityFrameworkCore;

namespace IntegrationTestSample
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Member { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasKey(r => r.Name);
        }
    }
}