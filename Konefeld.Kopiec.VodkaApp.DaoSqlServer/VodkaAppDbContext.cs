using Konefeld.Kopiec.VodkaApp.DaoSqlServer.BO;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Konefeld.Kopiec.VodkaApp.DaoSqlServer
{
    public class VodkaAppDbContext : DbContext
    {
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Vodka> Vodkas { get; set; }

        public VodkaAppDbContext(DbContextOptions<VodkaAppDbContext> options) : base(options)
        {
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
