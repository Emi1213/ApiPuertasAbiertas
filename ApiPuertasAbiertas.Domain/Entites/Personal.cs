namespace ApiPuertasAbiertas.Domain.Entities;

public class Personal
{
  public int Id { get; set; }
  public required string Nombres { get; set; }
  public required string Apellidos { get; set; }
  public required bool Estado { get; set; }
  public int EmpresaId { get; set; }
  public required Empresa Empresa { get; set; }
  public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
