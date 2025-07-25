namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

using ApiPuertasAbiertas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
{
  public void Configure(EntityTypeBuilder<Perfil> builder)
  {
    builder.ToTable("Perfiles_tbl");
    builder.HasKey(p => p.Id);
    builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(100);
    builder.Property(p => p.Descripcion)
        .HasMaxLength(500);

    builder.HasMany(p => p.Usuarios).WithOne().HasForeignKey(e => e.PerfilId);
  }
}