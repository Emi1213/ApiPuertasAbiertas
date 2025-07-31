using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiPuertasAbiertas.Domain.Entities;
namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
  public void Configure(EntityTypeBuilder<Usuario> builder)
  {
    builder.ToTable("Usuarios_tbl");

    builder.HasKey(u => u.Id);

    builder.Property(u => u.NombreUsuario)
        .HasColumnName("Usuario")
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

    builder.Property(u => u.PerfilId)
    .HasColumnName("Id_Perfil")
    .IsRequired();

    builder.HasOne(u => u.Perfil)
      .WithMany(p => p.Usuarios)
      .HasForeignKey(u => u.PerfilId)
      .OnDelete(DeleteBehavior.Cascade);

  }
}