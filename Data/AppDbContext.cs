using Microsoft.EntityFrameworkCore;
using BoletosApi.Models;

namespace BoletosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Boleto> Boletos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Codigo).IsRequired();
                entity.Property(e => e.PercentualJuros).IsRequired();
            });

            modelBuilder.Entity<Boleto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomePagador).IsRequired();
                entity.Property(e => e.CpfCnpjPagador).IsRequired();
                entity.Property(e => e.NomeBeneficiario).IsRequired();
                entity.Property(e => e.CpfCnpjBeneficiario).IsRequired();
                entity.Property(e => e.Valor).IsRequired();
                entity.Property(e => e.DataVencimento).IsRequired();
                entity.HasOne(e => e.Banco).WithMany().HasForeignKey(e => e.BancoId);
            });
        }
    }
}
