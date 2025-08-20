using ApiPuertasAbiertas.Application.DTOs.Auth;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Domain.Repositories;

namespace ApiPuertasAbiertas.Application.UseCases.Auth;

public class LoginUseCase
{
  private readonly IServicioAuth _servicioAuth;
  private readonly IUsuarioRepository _usuarioRepository;
  private readonly IActiveDirectoryServices _activeDirectoryServices;

  public LoginUseCase(IServicioAuth servicioAuth, IUsuarioRepository usuarioRepository, IActiveDirectoryServices activeDirectoryServices)
  {
    _servicioAuth = servicioAuth;
    _usuarioRepository = usuarioRepository;
    _activeDirectoryServices = activeDirectoryServices;
  }

  public async Task<LoginResponseDto?> ExecuteAsync(string usuario, string contrasenia)
  {
    bool credencialesValidasAD;
    try
    {
      credencialesValidasAD = _activeDirectoryServices.ValidateActiveDirectoryLogin(usuario, contrasenia);
    }
    catch (Exception)
    {
      return null;
    }

    if (!credencialesValidasAD)
    {
      return null;
    }

    var usuarioEncontrado = await _usuarioRepository.ObtenerPorNombreUsuarioAsync(usuario);

    if (usuarioEncontrado == null)
    {
      return null;
    }

    var token = _servicioAuth.GenerarToken(usuarioEncontrado);

    return new LoginResponseDto
    {
      Token = token,
      Expiracion = DateTime.UtcNow.AddHours(1)
    };
  }
}