using Konefeld.Kopiec.VodkaApp.DaoSqlite.BO;
using Microsoft.EntityFrameworkCore;

namespace Konefeld.Kopiec.VodkaApp.DaoSqlite
{
    public class VodkaAppDbContext : DbContext
    {
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Vodka> Vodkas { get; set; }

        public VodkaAppDbContext(DbContextOptions<VodkaAppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=..\\Database\\DaoSqlite.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vodka>()
                .HasOne(v => v.Producer)
                .WithMany()
                .HasForeignKey(v => v.ProducerId);
        }
    }
}
