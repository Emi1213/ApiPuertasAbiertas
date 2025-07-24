using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiPuertasAbiertas.Domain.Entities;
namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
  public void Configure(EntityTypeBuilder<Usuario> builder)
  {
    builder.ToTable("Usuarios");

    builder.HasKey(u => u.Id);

    builder.Property(u => u.NombreUsuario)
        .IsRequired()
        .HasMaxLength(50);

    builder.Property(u => u.Nombre)
        .IsRequired()
        .HasMaxLength(100);

    builder.Property(u => u.Descripcion)
        .HasMaxLength(500);

    builder.Property(u => u.Contrasenia)
        .IsRequired()
        .HasMaxLength(255);

    builder.HasOne(u => u.Perfil)
        .WithMany()
        .HasForeignKey(u => u.PerfilId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}