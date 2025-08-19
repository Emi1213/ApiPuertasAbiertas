using ApiPuertasAbiertas.Application.Converters;
using ApiPuertasAbiertas.Domain.Enums;
using System.Text.Json.Serialization;

namespace ApiPuertasAbiertas.Application.DTOs.Ingresos;

public class ActualizarIngresoDto
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
  public int? UsuarioReconId { get; set; }

  [JsonConverter(typeof(EstadoIngresoJsonConverter))]
  public EstadoIngreso Estado { get; set; } = EstadoIngreso.EnProceso;

  public int PersonalId { get; set; }
}
