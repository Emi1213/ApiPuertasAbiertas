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

  public async 
}