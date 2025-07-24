namespace ApiPuertasAbiertas.Domain.Entities;

public class Empresa
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public required string Estado { get; set; }
}
