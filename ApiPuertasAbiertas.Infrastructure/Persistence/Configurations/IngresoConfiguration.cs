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
    builder.Property(i => i.LlamadaRealizada)
        .HasColumnName("Llamada_Realizada")
        .IsRequired()
        .HasDefaultValue(false);
    builder.Property(i => i.Duracion)
          .HasColumnType("time");

    builder.Property(i => i.Comentario)
        .HasMaxLength(500);

    builder.Property(i => i.CodigoMotivo)
        .HasMaxLength(50)
        .HasColumnName("Codigo_Motivo");
    builder.Property(i => i.TipoMotivo)
        .HasMaxLength(50)
        .HasColumnName("Tipo_Motivo");
    builder.Property(i => i.FechaRecon)
        .HasColumnName("Fecha_Recon");
    builder.HasOne(i => i.Personal)
        .WithMany(p => p.Ingresos)
        .HasForeignKey(i => i.PersonalId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}