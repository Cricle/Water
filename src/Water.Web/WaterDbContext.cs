using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Water.Web.Models;

namespace Water.Web
{
    public class WaterDbContext : DbContext
    {
        public WaterDbContext(DbContextOptions options) : base(options)
        {
        }

        public WaterDbContext()
        {
        }

        public DbSet<WaterSession> Sessions => Set<WaterSession>();

        public DbSet<WaterSessionItem> SessionItems => Set<WaterSessionItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WaterSessionItem>(b =>
            {
                b.HasIndex(x => new { x.Name, x.SessionId });
                b.HasKey(x => new { x.Name, x.SessionId });
            });
        }
    }
}
