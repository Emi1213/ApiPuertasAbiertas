namespace ApiPuertasAbiertas.Application.Interfaces;

using ApiPuertasAbiertas.Application.DTOs.Auth;
using ApiPuertasAbiertas.Domain.Entities;

public interface IServicioAuth
{
  string GenerarToken(Usuario usuario);
}
