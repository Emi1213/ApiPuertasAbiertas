namespace ApiPuertasAbiertas.Infrastructure.Persistence.Configurations;

using ApiPuertasAbiertas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
  public void Configure(EntityTypeBuilder<Empresa> builder)
  {
    builder.ToTable("Empresas_tbl");
    
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Nombre)
        .IsRequired()
        .HasMaxLength(100);
    builder.Property(e => e.Estado)
        .IsRequired()
        .HasDefaultValue(true);

    builder.HasMany(e => e.Personal).WithOne().HasForeignKey(p => p.EmpresaId);
  }
}