using ApiPuertasAbiertas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

public class AlarmaConfiguration : IEntityTypeConfiguration<Alarma>
{
  public void Configure(EntityTypeBuilder<Alarma> builder)
  {
    builder.ToTable("Alarmas_tbl");

    builder.HasKey(a => a.Id);

    builder.Property(a => a.Id)
        .HasColumnName("Id")
        .ValueGeneratedOnAdd();

    builder.Property(a => a.IdIngreso)
        .HasColumnName("Id_Ingreso")
        .IsRequired(false);

    builder.Property(a => a.Nombre)
        .HasColumnName("Nombre")
        .HasMaxLength(100)
        .IsRequired(false);

    builder.Property(a => a.Estado)
        .HasColumnName("Estado")
        .HasMaxLength(50)
        .IsRequired(false);

    builder.Property(a => a.Fecha)
        .HasColumnName("Fecha")
        .HasColumnType("datetime2(7)")
        .IsRequired(false);

    builder.HasOne(a => a.Ingreso)
        .WithMany(i => i.Alarmas)
        .HasForeignKey(a => a.IdIngreso)
        .OnDelete(DeleteBehavior.SetNull);
  }
}
