using ApiPuertasAbiertas.Application.Extensions;
using ApiPuertasAbiertas.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiPuertasAbiertas.Application.Converters;

public class EstadoIngresoJsonConverter : JsonConverter<EstadoIngreso>
{
  public override EstadoIngreso Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var value = reader.GetString();
    return string.IsNullOrEmpty(value)
      ? EstadoIngreso.EnProceso
      : EstadoIngresoExtensions.FromDisplayName(value);
  }

  public override void Write(Utf8JsonWriter writer, EstadoIngreso value, JsonSerializerOptions options)
  {
    writer.WriteStringValue(value.ToDisplayName());
  }
}
