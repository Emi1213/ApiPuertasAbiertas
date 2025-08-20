using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using System.Security.Claims;

namespace ApiPuertasAbiertas.Application.Interfaces;

public interface IBuscarUsuariosActiveDirectoryUseCase
{
  Task<List<UsuarioActiveDirectoryDto>> ExecuteAsync(BusquedaActiveDirectoryRequestDto request, ClaimsPrincipal? user = null);
}
