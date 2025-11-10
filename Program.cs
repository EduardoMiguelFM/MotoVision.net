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
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MotoVision.API.Middleware;
using MotoVision.API.Services;
using MotoVision.API.Validations;
using MotoVision.Application.Interfaces;
using MotoVision.Application.Mapping;
using MotoVision.Infrastructure.Data;
using MotoVision.Domain.Repositories;
using MotoVision.Infrastructure.Repositories;
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
        // MONGODB CONFIGURATION
        // =======================================================
        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            var mongoConnection = builder.Configuration.GetConnectionString("MongoDB")
                                  ?? "mongodb://localhost:27017";
            return new MongoClient(mongoConnection);
        });

        builder.Services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase("MotoVisionDB"); // nome do banco MongoDB
        });

        // =======================================================
        // DEPENDENCY INJECTION
        // =======================================================
        builder.Services.AddScoped<IMotoRepository, MotoRepository>();
        builder.Services.AddScoped<IPatioRepository, PatioRepository>(); // ✅ Corrigido (antes era PatioService)
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
        // HEALTH CHECK
        // =======================================================
        builder.Services.AddHealthChecks()
            .AddCheck("hekath_check", () => HealthCheckResult.Healthy("API funcionando corretamente 🚀"))
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
        // JWT CONFIGURATION
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

        builder.Services.AddScoped<TokenService>(provider =>
            new TokenService(jwtKey,
                builder.Configuration["Jwt:Issuer"] ?? "MotoVision.API",
                builder.Configuration["Jwt:Audience"] ?? "MotoVision.Clients"));

        // =======================================================
        // ML.NET PREDICTION
        // =======================================================
        builder.Services.AddSingleton<MlPredictionService>();

        // =======================================================
        // API VERSIONING
        // =======================================================
        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // =======================================================
        // CONTROLLERS & MIDDLEWARE
        // =======================================================
        builder.Services.AddTransient<ErrorHandlingMiddleware>();
        builder.Services.AddControllers();

        // =======================================================
        // SWAGGER
        // =======================================================
        builder.Services.AddEndpointsApiExplorer();

        var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

        builder.Services.AddSwaggerGen(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"MotoVision API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "🚀 API RESTful para Gestão da Startup MotoVision",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe MotoVision",
                        Email = "contato@MotoVision.com.br"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            }

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

        var versionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in versionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        app.UseCors("AllowMobileApp");
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health");

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
            await SeedData.SeedAsync(db);
        }

        app.Run();
    }
}
