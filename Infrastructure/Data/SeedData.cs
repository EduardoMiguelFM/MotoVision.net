using Mottu.Domain.Entities;
using Mottu.Domain.ValueObjects;

namespace Mottu.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Patios.Any())
            {
                var patios = new List<Patio>
                {
                    new Patio("Pátio Butantã", "Rua das Flores, 123"),
                    new Patio("Pátio Vila Madalena", "Av. Paulista, 456"),
                    new Patio("Pátio Pinheiros", "Rua Augusta, 789")
                };

                context.Patios.AddRange(patios);
                await context.SaveChangesAsync();
            }

            if (!context.Usuarios.Any())
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario
                    {
                        Nome = "Admin",
                        Email = "admin@mottu.com.br",
                        Senha = "admin123",
                        CPF = "123.456.789-00",
                        Funcao = "Administrador"
                    },
                    new Usuario
                    {
                        Nome = "Supervisor",
                        Email = "supervisor@mottu.com.br",
                        Senha = "super123",
                        CPF = "987.654.321-00",
                        Funcao = "Supervisor"
                    },
                    new Usuario
                    {
                        Nome = "Operador",
                        Email = "operador@mottu.com.br",
                        Senha = "oper123",
                        CPF = "456.789.123-00",
                        Funcao = "Operador"
                    }
                };

                context.Usuarios.AddRange(usuarios);
                await context.SaveChangesAsync();
            }

            if (!context.Motos.Any())
            {
                var patios = context.Patios.ToList();
                var motos = new List<Moto>();

                // Moto 1 - Honda Biz - DISPONIVEL
                var moto1 = new Moto("Honda Biz", new Placa("ABC1234"), patios[0]);
                moto1.DefinirStatus(Domain.Enums.StatusMoto.DISPONIVEL);
                motos.Add(moto1);

                // Moto 2 - Yamaha Factor - MANUTENCAO
                var moto2 = new Moto("Yamaha Factor", new Placa("DEF5678"), patios[1]);
                moto2.DefinirStatus(Domain.Enums.StatusMoto.MANUTENCAO);
                motos.Add(moto2);

                // Moto 3 - Honda CG - RESERVADA
                var moto3 = new Moto("Honda CG", new Placa("GHI9012"), patios[2]);
                moto3.DefinirStatus(Domain.Enums.StatusMoto.RESERVADA);
                motos.Add(moto3);

                // Moto 4 - Suzuki GS - FALTA_PECA
                var moto4 = new Moto("Suzuki GS", new Placa("JKL3456"), patios[0]);
                moto4.DefinirStatus(Domain.Enums.StatusMoto.FALTA_PECA);
                motos.Add(moto4);

                // Moto 5 - Kawasaki Ninja - INDISPONIVEL
                var moto5 = new Moto("Kawasaki Ninja", new Placa("MNO7890"), patios[1]);
                moto5.DefinirStatus(Domain.Enums.StatusMoto.INDISPONIVEL);
                motos.Add(moto5);

                context.Motos.AddRange(motos);
                await context.SaveChangesAsync();
            }
        }
    }
}
