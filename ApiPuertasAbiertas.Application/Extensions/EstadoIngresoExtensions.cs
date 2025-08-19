using ApiPuertasAbiertas.Domain.Enums;

namespace ApiPuertasAbiertas.Application.Extensions;

public static class EstadoIngresoExtensions
{
  public static string ToDisplayName(this EstadoIngreso estado)
  {
    return estado switch
    {
      EstadoIngreso.EnProceso => "En Proceso",
      EstadoIngreso.RegistroAlarma => "Registro Alarma",
      EstadoIngreso.Cerrado => "Cerrado",
      EstadoIngreso.AlarmaDescompuesta => "Alarma Descompuesta",
      _ => estado.ToString()
    };
  }

  public static EstadoIngreso FromDisplayName(string displayName)
  {
    return displayName switch
    {
      "En Proceso" => EstadoIngreso.EnProceso,
      "Registro Alarma" => EstadoIngreso.RegistroAlarma,
      "Cerrado" => EstadoIngreso.Cerrado,
      "Alarma Descompuesta" => EstadoIngreso.AlarmaDescompuesta,
      _ => Enum.TryParse<EstadoIngreso>(displayName, out var result) ? result : EstadoIngreso.EnProceso
    };
  }
}
