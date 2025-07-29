using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ApiPuertasAbiertas.API.Middleware;
using System.Text;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Infrastructure.Services;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ApiPuertasAbiertas.Application.UseCases.Auth;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Repositories;
using ApiPuertasAbiertas.Application.UseCases.Usuarios;
using ApiPuertasAbiertas.Application.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IServicioAuth, ServicioAuth>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<UsuarioUseCases>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UsuarioProfile));
builder.Services.AddAutoMapper(typeof(PerfilProfile));


builder.Services.AddResponseCompression(options =>
{
  options.EnableForHttps = true;
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var clave = jwtSettings["Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave!))
      };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStandardResponseWrapper();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
