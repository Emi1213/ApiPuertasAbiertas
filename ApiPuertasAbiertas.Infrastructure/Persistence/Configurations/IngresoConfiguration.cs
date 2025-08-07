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
    builder.Property(i => i.Duracion);

    builder.Property(i => i.Comentario)
        .HasMaxLength(500);

    builder.Property(i => i.IdMotivo)
        .HasMaxLength(50)
        .HasColumnName("Id_Motivo");
    builder.Property(i => i.TipoMotivo)
        .HasMaxLength(50)
        .HasColumnName("Tipo_Motivo");
    builder.Property(i => i.FechaRecon)
        .HasColumnName("Fecha_Recon");
    builder.Property(i => i.Causa)
        .HasMaxLength(200);
    builder.Property(i => i.UsuarioRecon)
        .HasColumnName("Usuario_Recon");
    builder.Property(i => i.Estado)
        .HasMaxLength(50);
    builder.HasOne(i => i.Personal)
        .WithMany(p => p.Ingresos)
        .HasForeignKey(i => i.PersonalId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}