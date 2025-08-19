namespace ApiPuertasAbiertas.Application.DTOs.Ingresos;

public class AlarmaDto
{
  public int Id { get; set; }
  public int? IdIngreso { get; set; }
  public string? Nombre { get; set; }
  public string? Estado { get; set; }
  public DateTime? Fecha { get; set; }
}
