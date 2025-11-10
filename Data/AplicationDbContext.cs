using Microsoft.EntityFrameworkCore;
using MotoVision.Domain.Entities; // onde Patio, Moto, UsuarioPatio estão definidos
using MotoVision.Domain.ValueObjects; 

namespace MotoVision.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<UsuarioPatio> UsuariosPatio { get; set; }

        
    }
}
