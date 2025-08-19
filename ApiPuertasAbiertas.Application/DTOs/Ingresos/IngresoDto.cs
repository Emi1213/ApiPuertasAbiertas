using ApiPuertasAbiertas.Application.Converters;
using ApiPuertasAbiertas.Application.DTOs.Personal;
using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Domain.Enums;
using System.Text.Json.Serialization;

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
  public int? UsuarioReconId { get; set; }
  public UsuarioDto? UsuarioRecon { get; set; }

  [JsonConverter(typeof(EstadoIngresoJsonConverter))]
  public EstadoIngreso Estado { get; set; } = EstadoIngreso.EnProceso;

  public PersonalDto? Personal { get; set; }
  public List<AlarmaDto> Alarmas { get; set; } = new List<AlarmaDto>();
}