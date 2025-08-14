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

    builder.HasMany(p => p.Usuarios)
        .WithOne(u => u.Perfil)
        .HasForeignKey(u => u.PerfilId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(p => p.Modulos)
        .WithMany(m => m.Perfiles)
        .UsingEntity<ModuloPerfil>(
          j => j.HasOne(mp => mp.Modulo)
          .WithMany(m => m.ModulosPerfiles)
          .HasForeignKey(mp => mp.ModuloId)
          .OnDelete(DeleteBehavior.Cascade),
          j => j.HasOne(mp => mp.Perfil)
          .WithMany(p => p.ModulosPerfiles)
          .HasForeignKey(mp => mp.PerfilId)
          .OnDelete(DeleteBehavior.Cascade),
          j =>
          {
            j.ToTable("Modulos_Perfiles_tbl");
            j.HasKey(mp => mp.Id);
            j.Property(mp => mp.ModuloId).HasColumnName("Id_Modulo");
            j.Property(mp => mp.PerfilId).HasColumnName("Id_Perfil");
            j.HasIndex(mp => new { mp.ModuloId, mp.PerfilId }).IsUnique();
          });
  }
}