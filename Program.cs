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
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

// DI
builder.Services.AddScoped<IMotoRepository, MotoService>();
builder.Services.AddScoped<IPatioRepository, PatioService>();
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
builder.Services.AddSwaggerGen();


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
}
