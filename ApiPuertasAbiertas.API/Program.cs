using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ApiPuertasAbiertas.API.Middleware;
using ApiPuertasAbiertas.API.Extensions;
using System.Text;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddConfigurationOptions(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAutoMapperProfiles();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new() { Title = "API Puertas Abiertas", Version = "v1" });

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
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
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
