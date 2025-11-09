using Microsoft.EntityFrameworkCore;
using Mottu.API.Controllers; // importa a classe Usuario

namespace Mottu.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
