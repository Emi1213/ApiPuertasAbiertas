using ApiPuertasAbiertas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

public class ModuloPerfilConfiguration : IEntityTypeConfiguration<ModuloPerfil>
{
  public void Configure(EntityTypeBuilder<ModuloPerfil> builder)
  {
    builder.ToTable("Modulos_Perfiles_tbl");
    builder.HasKey(mp => mp.Id);

    builder.Property(mp => mp.ModuloId).HasColumnName("Id_Modulo");
    builder.Property(mp => mp.PerfilId).HasColumnName("Id_Perfil");

    builder.HasOne(mp => mp.Modulo)
          .WithMany(m => m.ModulosPerfiles)
          .HasForeignKey(mp => mp.ModuloId)
          .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(mp => mp.Perfil)
          .WithMany(p => p.ModulosPerfiles)
          .HasForeignKey(mp => mp.PerfilId)
          .OnDelete(DeleteBehavior.Cascade);

    builder.HasIndex(mp => new { mp.ModuloId, mp.PerfilId }).IsUnique();
  }
}