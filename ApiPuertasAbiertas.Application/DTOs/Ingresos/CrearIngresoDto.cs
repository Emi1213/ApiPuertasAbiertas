namespace ApiPuertasAbiertas.Application.DTOs.Ingresos;

public class CrearIngresoDto
{
  public required DateTime FechaInicio { get; set; }
  public DateTime? FechaFin { get; set; }
  public string? Comentario { get; set; }
  public string? IdMotivo { get; set; }
  public string? TipoMotivo { get; set; } 
  public string? Causa { get; set; }
  public required string Estado { get; set; } = "En proceso";
  public int PersonalId { get; set; }
}
