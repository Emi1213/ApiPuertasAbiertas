namespace ApiPuertasAbiertas.Application.Interfaces;

using ApiPuertasAbiertas.Application.DTOs.Auth;

public interface IServicioAuth
{
  Task<AuthResponseDto?> AutenticarAsync(LoginDto dto);
}
