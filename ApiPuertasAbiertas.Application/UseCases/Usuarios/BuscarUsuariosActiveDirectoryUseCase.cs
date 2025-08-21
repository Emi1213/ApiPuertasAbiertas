using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Application.Interfaces;
using System.Security.Claims;

namespace ApiPuertasAbiertas.Application.UseCases.Usuarios;

public class BuscarUsuariosActiveDirectoryUseCase : IBuscarUsuariosActiveDirectoryUseCase
{
  private readonly IActiveDirectoryServices _activeDirectoryServices;

  public BuscarUsuariosActiveDirectoryUseCase(IActiveDirectoryServices activeDirectoryServices)
  {
    _activeDirectoryServices = activeDirectoryServices;
  }

  public async Task<List<UsuarioActiveDirectoryDto>> ExecuteAsync(BusquedaActiveDirectoryRequestDto request, ClaimsPrincipal? usuario = null)
  {
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

    return await Task.Run(() =>
    {
      try
      {
        cts.Token.ThrowIfCancellationRequested();
        return _activeDirectoryServices.SearchUsersTop10(request.Query);
      }
      catch (OperationCanceledException)
      {
        throw new TimeoutException("La búsqueda en Active Directory excedió el tiempo límite de 5 segundos");
      }
    }, cts.Token);
  }
}
