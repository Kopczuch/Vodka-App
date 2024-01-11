using Konefeld.Kopiec.VodkaApp.DaoSqlite.BO;
using Microsoft.EntityFrameworkCore;

namespace Konefeld.Kopiec.VodkaApp.DaoSqlite
{
    public class VodkaAppDbContext : DbContext
    {
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Vodka> Vodkas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=Database\\DaoSqlite.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vodka>()
                .HasOne(v => v.ProducerImpl)
                .WithMany()
                .HasForeignKey(v => v.ProducerId);
        }
    }
}
