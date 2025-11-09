using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer; // NECESSÁRIO PARA SWAGGER E VERSÕES
using Microsoft.AspNetCore.Mvc.Versioning; // NECESSÁRIO PARA VERSIONAMENTO
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mottu.API.Middleware;
using Mottu.API.Services;
using Mottu.API.Validations;
using Mottu.Application.Interfaces;
using Mottu.Application.Mapping;
using MotoVision.net.Data;
using Mottu.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

[CompilerGenerated]
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // =======================================================
        // DATABASE (PostgreSQL)
        // =======================================================
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
        });

        // =======================================================
        // DEPENDENCY INJECTION
        // =======================================================
        builder.Services.AddScoped<IMotoRepository, MotoService>();
        builder.Services.AddScoped<IPatioRepository, PatioService>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioService>();
        builder.Services.AddScoped<IUsuarioPatioRepository, UsuarioPatioService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // =======================================================
        // AUTOMAPPER & VALIDATION
        // =======================================================
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<MotoDTOValidator>();

        // =======================================================
        // HEALTH CHECK (10 pts)
        // =======================================================
        builder.Services.AddHealthChecks()
            // Adiciona uma verificação customizada
            .AddCheck("hekath_check", () => HealthCheckResult.Healthy("API funcionando corretamente 🚀"))
            // Adiciona uma verificação de conexão com o Banco de Dados
            .AddDbContextCheck<ApplicationDbContext>(name: "database_check");

        // =======================================================
        // CORS
        // =======================================================
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowMobileApp",
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        // =======================================================
        // JWT CONFIGURATION (25 pts na Configuração)
        // =======================================================
        var jwtKey = builder.Configuration["Jwt:Key"] ?? "chave-super-secreta";
        var key = Encoding.ASCII.GetBytes(jwtKey);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        // Injeção do TokenService, passando a chave secreta diretamente
        builder.Services.AddScoped<TokenService>(provider =>
            new TokenService(jwtKey, builder.Configuration["Jwt:Issuer"] ?? "Mottu.API", builder.Configuration["Jwt:Audience"] ?? "Mottu.Clients"));

        // =======================================================
        // ML.NET PREDICTION (25 pts)
        // =======================================================
        builder.Services.AddSingleton<MlPredictionService>(); // Usar Singleton pois o modelo só precisa ser carregado uma vez

        // =======================================================
        // API VERSIONING (10 pts)
        // =======================================================
        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.ReportApiVersions = true; // Inclui a versão nos headers de resposta
        })
        .AddApiExplorer(options =>
        {
            // Formato 'v{MajorVersion}'
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // =======================================================
        // CONTROLLERS & MIDDLEWARE
        // =======================================================
        builder.Services.AddTransient<ErrorHandlingMiddleware>();
        builder.Services.AddControllers();

        // =======================================================
        // SWAGGER (Atualizado para Versionamento)
        // =======================================================
        builder.Services.AddEndpointsApiExplorer();

        // Resolve o provedor de descrição de versão para usar no Swagger
        var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

        builder.Services.AddSwaggerGen(c =>
        {
            // Loop para criar um documento de Swagger por versão
            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Mottu API {description.ApiVersion}", // Título dinâmico por versão
                    Version = description.ApiVersion.ToString(),
                    Description = @"
## 🚀 API RESTful para Gestão da Startup Mottu...",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe Mottu",
                        Email = "contato@mottu.com.br"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            }

            // JWT no Swagger (mantido)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insira o token JWT desta forma: Bearer {seu_token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);

            c.UseInlineDefinitionsForEnums();
        });

        // =======================================================
        // BUILD APP
        // =======================================================
        var app = builder.Build();

        // Middleware order matters

        // Atualizado para suportar o seletor de versão no Swagger UI
        var versionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // Cria um endpoint na UI para cada versão
            foreach (var description in versionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        app.UseCors("AllowMobileApp");

        app.UseMiddleware<ErrorHandlingMiddleware>();

        // Middleware de Autenticação e Autorização (Ordem Correta: Authentication antes de Authorization)
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Mapeamento do Health Check (10 pts)
        app.MapHealthChecks("/health");

        // Database seeding
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
            await SeedData.SeedAsync(db);
        }

        app.Run();
    }
}