using ApiPuertasAbiertas.Application.Converters;
using ApiPuertasAbiertas.Domain.Enums;
using System.Text.Json.Serialization;

namespace ApiPuertasAbiertas.Application.DTOs.Ingresos;

public class BuscarIngresosQuery
{
  public string? busqueda { get; set; }

  [JsonConverter(typeof(EstadoIngresoJsonConverter))]
  public EstadoIngreso? estado { get; set; }

  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}