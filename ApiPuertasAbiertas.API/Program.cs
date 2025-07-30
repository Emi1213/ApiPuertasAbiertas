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
using Microsoft.AspNetCore.Mvc;
using ApiPuertasAbiertas.Shared.Responses;
using ApiPuertasAbiertas.Application.UseCases.Empresas;
using ApiPuertasAbiertas.Application.UseCases.Personal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IServicioAuth, ServicioAuth>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<UsuarioUseCases>();
builder.Services.AddScoped<EmpresaUseCases>();
builder.Services.AddScoped<PersonalUseCases>();
builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();


builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new() { Title = "API Puertas Abiertas", Version = "v1" });

  // 🔐 Configuración JWT
  c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    Description = "Ingrese el token JWT con el esquema Bearer. Ejemplo: **Bearer eyJhbGciOi...**"
  });

  c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll",
    policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(UsuarioProfile));
builder.Services.AddAutoMapper(typeof(PerfilProfile));


builder.Services.AddResponseCompression(options =>
{
  options.EnableForHttps = true;
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var clave = jwtSettings["Key"];
Console.WriteLine("🔐 Clave validación: " + clave);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        // ValidIssuer = jwtSettings["Issuer"],
        // ValidAudience = jwtSettings["Audience"],
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
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseStandardResponseWrapper();
app.MapControllers();

app.Run();
