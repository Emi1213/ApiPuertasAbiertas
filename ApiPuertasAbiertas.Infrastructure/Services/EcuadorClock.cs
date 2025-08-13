using ApiPuertasAbiertas.Application.Interfaces;

namespace ApiPuertasAbiertas.Infrastructure.Services;

public class EcuadorClock : IClock
{
  private static readonly TimeZoneInfo EcuadorTimeZone =
      TimeZoneInfo.CreateCustomTimeZone("Ecuador", TimeSpan.FromHours(-5), "Ecuador Standard Time", "ECT");

  public DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, EcuadorTimeZone);
}
