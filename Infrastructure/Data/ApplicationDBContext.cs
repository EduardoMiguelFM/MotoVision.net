using Microsoft.EntityFrameworkCore;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Moto> Motos => Set<Moto>();
        public DbSet<Patio> Patios => Set<Patio>();
        public DbSet<UsuarioPatio> UsuariosPatio => Set<UsuarioPatio>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Moto>(b =>
            {
                b.ToTable("MOTOS");
                b.HasKey(x => x.Id);
                b.Property(x => x.Modelo).IsRequired().HasMaxLength(100);

                // Enum como string
                b.Property(x => x.Status)
                 .HasConversion<string>()
                 .HasColumnName("STATUS")
                 .IsRequired();

                // VO: Placa
                b.OwnsOne(x => x.Placa, p =>
                {
                    p.Property(v => v.Valor)
                     .HasColumnName("PLACA")
                     .HasMaxLength(7)
                     .IsRequired();
                });

                // VO: SetorCor
                b.OwnsOne(x => x.SetorCor, sc =>
                {
                    sc.Property(v => v.Setor).HasColumnName("SETOR").HasMaxLength(20);
                    sc.Property(v => v.Cor).HasColumnName("COR").HasMaxLength(20);
                });

                b.HasOne(x => x.Patio)
                 .WithMany(p => p.Motos)
                 .HasForeignKey(x => x.PatioId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Patio>(b =>
            {
                b.ToTable("PATIOS");
                b.HasKey(x => x.Id);
                b.Property(x => x.Nome).IsRequired().HasMaxLength(120);
            });

            mb.Entity<UsuarioPatio>(b =>
            {
                b.ToTable("USUARIOS_PATIO");
                b.HasKey(x => x.Id);
                b.Property(x => x.Nome).IsRequired().HasMaxLength(120);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.Funcao).IsRequired().HasMaxLength(60);

                b.HasOne(x => x.Patio)
                 .WithMany()
                 .HasForeignKey(x => x.PatioId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}