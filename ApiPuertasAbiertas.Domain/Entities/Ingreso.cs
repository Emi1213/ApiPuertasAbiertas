using ApiPuertasAbiertas.Domain.Enums;

namespace ApiPuertasAbiertas.Domain.Entities;

public class Ingreso
{
  public int Id { get; set; }
  public required DateTime FechaInicio { get; set; }
  public DateTime? FechaFin { get; set; }
  public required bool LlamadaRealizada { get; set; }
  public TimeSpan? Duracion { get; set; }
  public string? Comentario { get; set; }
  public string? CodigoMotivo { get; set; }
  public TipoMotivo? TipoMotivo { get; set; }
  public DateTime? FechaRecon { get; set; }
  public int PersonalId { get; set; }
  public required Personal Personal { get; set; }

}