using ApiPuertasAbiertas.Domain.Enums;

namespace ApiPuertasAbiertas.Domain.Entities;

public class Ingreso
{
  public int Id { get; set; }
  public required DateTime FechaInicio { get; set; }
  public DateTime? FechaFin { get; set; }
  public required bool LlamadaRealizada { get; set; }
  public string? Duracion { get; set; }
  public string? Comentario { get; set; }
  public string? IdMotivo { get; set; }
  public TipoMotivo? TipoMotivo { get; set; }
  public string? Causa { get; set; }
  public DateTime? FechaRecon { get; set; }
  public required string UsuarioRecon { get; set; } = string.Empty;
  public required string Estado { get; set; } = "En proceso";
  public int PersonalId { get; set; }
  public required Personal Personal { get; set; }

}