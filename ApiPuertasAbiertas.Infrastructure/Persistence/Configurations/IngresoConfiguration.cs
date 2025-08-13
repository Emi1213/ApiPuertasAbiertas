namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

using ApiPuertasAbiertas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class IngresoConfiguration : IEntityTypeConfiguration<Ingreso>
{
  public void Configure(EntityTypeBuilder<Ingreso> builder)
  {
    builder.ToTable("Ingresos_tbl");
    builder.HasKey(i => i.Id);

    builder.Property(i => i.FechaInicio)
        .HasColumnName("Fecha_Inicio")
        .IsRequired();

    builder.Property(i => i.FechaFin)
        .HasColumnName("Fecha_Fin");

    builder.Property(i => i.Duracion)
        .HasColumnName("Duracion");

    builder.Property(i => i.Comentario)
        .HasColumnName("Comentario")
        .HasMaxLength(500);

    builder.Property(i => i.IdMotivo)
        .HasColumnName("Id_Motivo")
        .HasMaxLength(50);

    builder.Property(i => i.TipoMotivo)
        .HasColumnName("Tipo_Motivo")
        .HasMaxLength(100);

    builder.Property(i => i.FechaRecon)
        .HasColumnName("Fecha_Recon");

    builder.Property(i => i.Causa)
        .HasColumnName("Causa")
        .HasMaxLength(250);

    builder.Property(i => i.UsuarioReconId)
        .HasColumnName("Id_Usuario");

    builder.Property(i => i.Estado)
        .HasColumnName("Estado")
        .HasMaxLength(20);

    builder.Property(i => i.PersonalId)
        .HasColumnName("Id_Personal")
        .IsRequired();

    builder.HasOne(i => i.Personal)
        .WithMany(p => p.Ingresos)
        .HasForeignKey(i => i.PersonalId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(i => i.UsuarioRecon)
        .WithMany(u => u.IngresosReconocidos)
        .HasForeignKey(i => i.UsuarioReconId)
        .OnDelete(DeleteBehavior.SetNull);
  }
}