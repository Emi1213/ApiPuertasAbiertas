using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

  public DbSet<Usuario> Usuarios => Set<Usuario>();
  public DbSet<Empresa> Empresas => Set<Empresa>();
  public DbSet<Personal> Personal => Set<Personal>();
  public DbSet<Perfil> Perfiles => Set<Perfil>();
  public DbSet<Ingreso> Ingresos => Set<Ingreso>();
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
    modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
    modelBuilder.ApplyConfiguration(new PersonalConfiguration());
    modelBuilder.ApplyConfiguration(new PerfilConfiguration());
    modelBuilder.ApplyConfiguration(new IngresoConfiguration());
  }
}


