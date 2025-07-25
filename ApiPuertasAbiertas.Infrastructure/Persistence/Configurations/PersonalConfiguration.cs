namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

using ApiPuertasAbiertas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class PersonalConfiguration : IEntityTypeConfiguration<Personal>
{
  public void Configure(EntityTypeBuilder<Personal> builder)
  {
    builder.ToTable("Personal_tbl");

    builder.HasKey(p => p.Id);

    builder.Property(p => p.Nombres)
        .IsRequired()
        .HasMaxLength(100);
    builder.Property(p => p.Apellidos)
        .IsRequired()
        .HasMaxLength(100);
    builder.Property(p => p.Estado)
        .IsRequired()
        .HasDefaultValue(true);

    builder.HasOne(p => p.Empresa)
        .WithMany(e => e.Personal)
        .HasForeignKey(p => p.EmpresaId);
  }
}