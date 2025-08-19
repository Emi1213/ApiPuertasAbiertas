using ApiPuertasAbiertas.API.Realtime;
using ApiPuertasAbiertas.Shared.Interfaces;
using Microsoft.AspNetCore.SignalR;

public class RbacNotifier : IRbacNotifier
{
  private readonly IHubContext<RbacHub> _hub;
  public RbacNotifier(IHubContext<RbacHub> hub) => _hub = hub;

  public async Task NotificarCambioModulosAsync(int perfilId)
  {
    await _hub.Clients.Group($"role:{perfilId}")
        .SendAsync("roleModulesChanged", new { perfilId });
  }
}