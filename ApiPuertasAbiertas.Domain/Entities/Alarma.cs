namespace ApiPuertasAbiertas.Domain.Entities;

public class Alarma
{
  public int Id { get; set; }
  public int? IdIngreso { get; set; }
  public string? Nombre { get; set; }
  public string? Estado { get; set; }
  public DateTime? Fecha { get; set; }
  public virtual Ingreso? Ingreso { get; set; }
}
