namespace ApiPuertasAbiertas.Domain.Entities;

public class Empresa
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public required bool Estado { get; set; }
  public ICollection<Personal> Personal { get; set; } = new List<Personal>();
}
