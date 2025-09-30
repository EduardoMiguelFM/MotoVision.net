using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Mottu.API.Middleware;
using Mottu.API.Services;
using Mottu.API.Validations;
using Mottu.Application.Interfaces;
using Mottu.Application.Mapping;
using Mottu.Infrastructure.Data;
using Mottu.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// DI
builder.Services.AddScoped<IMotoRepository, MotoService>();
builder.Services.AddScoped<IPatioRepository, PatioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioService>();
builder.Services.AddScoped<IUsuarioPatioRepository, UsuarioPatioService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Validation + Middleware
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<MotoDTOValidator>();
builder.Services.AddTransient<ErrorHandlingMiddleware>();

// Swagger + MVC
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Mottu API",
        Version = "v1",
        Description = @"
## 🚀 API RESTful para Gestão da Startup Mottu

Sistema de compartilhamento de motos que gerencia:
- **Motos**: Cadastro, status automático e localização por setores
- **Pátios**: Locais de estacionamento das motos
- **Usuários**: Funcionários do sistema

### 🔄 Sistema de Status Automático
O sistema define automaticamente o setor e cor da moto baseado no status:
- DISPONIVEL → Setor A (Verde)
- RESERVADA → Setor B (Azul)  
- MANUTENCAO → Setor C (Amarelo)
- FALTA_PECA → Setor D (Laranja)
- INDISPONIVEL → Setor E (Cinza)
- DANOS_ESTRUTURAIS → Setor F (Vermelho)
- SINISTRO → Setor G (Preto)

### 👥 Equipe
- Eduardo Miguel Forato Monteiro – RM 555871
- Cícero Gabriel Oliveira Serafim – RM 556996
- Murillo Ari Ferreira Sant'Anna – RM 557183
        ",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe Mottu",
            Email = "contato@mottu.com.br"
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Incluir comentários XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Adicionar suporte para enums como strings
    c.UseInlineDefinitionsForEnums();
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.Run();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    await Mottu.Infrastructure.Data.SeedData.SeedAsync(db);
}
