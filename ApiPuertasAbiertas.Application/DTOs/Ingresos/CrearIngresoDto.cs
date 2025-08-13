namespace ApiPuertasAbiertas.Application.DTOs.Ingresos;

public class CrearIngresoDto
{
  public required DateTime FechaInicio { get; set; }
  public DateTime? FechaFin { get; set; }
  public required int PersonalId { get; set; }
  public string? IdMotivo { get; set; }
  public string? Comentario { get; set; }
}
