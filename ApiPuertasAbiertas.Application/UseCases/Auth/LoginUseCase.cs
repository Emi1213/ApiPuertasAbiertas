using ApiPuertasAbiertas.Application.DTOs.Auth;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Domain.Repositories;

namespace ApiPuertasAbiertas.Application.UseCases.Auth;

public class LoginUseCase
{
  private readonly IServicioAuth _servicioAuth;
  private readonly IUsuarioRepository _usuarioRepository;

  public LoginUseCase(IServicioAuth servicioAuth, IUsuarioRepository usuarioRepository)
  {
    _servicioAuth = servicioAuth;
    _usuarioRepository = usuarioRepository;
  }

  public async Task<LoginResponseDto?> ExecuteAsync(string usuario, string contrasenia)
  {
    var usuarioEncontrado = await _usuarioRepository.BuscarPorCredencialesAsync(usuario, contrasenia);

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