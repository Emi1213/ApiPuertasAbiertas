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
    string nombreUsuario = request.Usuario ?? "";
    string contrasenia = request.Contrasenia ?? "";

    if (string.IsNullOrWhiteSpace(nombreUsuario) && usuario != null)
    {
      var claimNombreUsuario = usuario.FindFirst(ClaimTypes.Name);
      if (claimNombreUsuario != null)
      {
        nombreUsuario = claimNombreUsuario.Value;
      }
    }

    if (string.IsNullOrWhiteSpace(nombreUsuario))
    {
      throw new ArgumentException("Se requiere usuario para realizar la búsqueda. Proporcione 'usuario' y 'contrasenia' como query parameters.");
    }

    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

    return await Task.Run(() =>
    {
      try
      {
        cts.Token.ThrowIfCancellationRequested();
        return _activeDirectoryServices.SearchUsersTop10(nombreUsuario, contrasenia, request.Query);
      }
      catch (OperationCanceledException)
      {
        throw new TimeoutException("La búsqueda en Active Directory excedió el tiempo límite de 5 segundos");
      }
    }, cts.Token);
  }
}
