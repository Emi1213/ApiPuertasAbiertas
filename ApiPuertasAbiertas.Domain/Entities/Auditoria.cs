namespace ApiPuertasAbiertas.Domain.Entities;

public class Auditoria
{
  public int Id { get; set; }
  public required string Entidad { get; set; }
  public required string Accion { get; set; }
  public required string Descripcion { get; set; }
  public required DateTime Fecha { get; set; }
  public required string Usuario { get; set; }
}
