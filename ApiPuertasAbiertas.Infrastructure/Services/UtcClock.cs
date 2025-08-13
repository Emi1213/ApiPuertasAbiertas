using ApiPuertasAbiertas.Application.Interfaces;

namespace ApiPuertasAbiertas.Infrastructure.Services;

public class UtcClock : IClock
{
  public DateTime Now => DateTime.UtcNow;
}
