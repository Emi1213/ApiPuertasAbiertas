using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Application.Interfaces;
using System.Security.Claims;

namespace ApiPuertasAbiertas.Application.UseCases.Usuarios;

public class BuscarUsuariosActiveDirectoryUseCase
{
  private readonly IActiveDirectoryServices _activeDirectoryServices;

  public BuscarUsuariosActiveDirectoryUseCase(IActiveDirectoryServices activeDirectoryServices)
  {
    _activeDirectoryServices = activeDirectoryServices;
  }

  public async Task<List<UsuarioActiveDirectoryDto>> ExecuteAsync(BusquedaActiveDirectoryRequestDto request, ClaimsPrincipal? user = null)
  {
    string username = request.Usuario ?? "";
    string password = request.Contrasenia ?? "";

    if (string.IsNullOrWhiteSpace(username) && user != null)
    {
      var userNameClaim = user.FindFirst(ClaimTypes.Name);
      if (userNameClaim != null)
      {
        username = userNameClaim.Value;
      }
    }

    if (string.IsNullOrWhiteSpace(username))
    {
      throw new ArgumentException("Se requiere usuario para realizar la búsqueda. Proporcione 'usuario' y 'contrasenia' como query parameters.");
    }

    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

    return await Task.Run(() =>
    {
      try
      {
        cts.Token.ThrowIfCancellationRequested();
        return _activeDirectoryServices.SearchUsersTop10(username, password, request.Query);
      }
      catch (OperationCanceledException)
      {
        throw new TimeoutException("La búsqueda en Active Directory excedió el tiempo límite de 5 segundos");
      }
    }, cts.Token);
  }
}
