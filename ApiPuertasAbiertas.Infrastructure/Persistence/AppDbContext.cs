using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

  public DbSet<Usuario> Usuarios => Set<Usuario>();
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
  }
}


