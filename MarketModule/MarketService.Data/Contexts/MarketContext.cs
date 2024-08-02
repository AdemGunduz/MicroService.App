using MarketService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketService.Data.Contexts
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }

        public DbSet<Market> Markets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18, 2)"); 
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
