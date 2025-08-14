using ApiPuertasAbiertas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

public class ModuloConfiguration : IEntityTypeConfiguration<Modulo>
{
  public void Configure(EntityTypeBuilder<Modulo> builder)
  {
    builder.ToTable("Modulos_tbl");

    builder.HasKey(m => m.Id);

    builder.Property(m => m.Nombre)
          .IsRequired()
          .HasMaxLength(100);
    builder.Property(m => m.Descripcion)
          .HasMaxLength(500);
    
    builder.HasMany
  }
}