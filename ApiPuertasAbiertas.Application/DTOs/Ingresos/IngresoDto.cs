using ApiPuertasAbiertas.Application.DTOs.Personal;

namespace ApiPuertasAbiertas.Application.DTOs.Ingresos;

public class IngresoDto
{
  public int Id { get; set; }
  public required DateTime FechaInicio { get; set; }
  public DateTime? FechaFin { get; set; }
  public string? Duracion { get; set; }
  public string? Comentario { get; set; }
  public string? IdMotivo { get; set; }
  public string? TipoMotivo { get; set; }
  public string? Causa { get; set; }
  public DateTime? FechaRecon { get; set; }
  public string? UsuarioRecon { get; set; }
  public required string Estado { get; set; } = "En proceso";
  public PersonalDto? Personal { get; set; }
}