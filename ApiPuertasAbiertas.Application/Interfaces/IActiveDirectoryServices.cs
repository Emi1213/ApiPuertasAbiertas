using ApiPuertasAbiertas.Application.DTOs.Usuarios;

namespace ApiPuertasAbiertas.Application.Interfaces;

public interface IActiveDirectoryServices
{
  bool ValidateActiveDirectoryLogin(string nombreUsuario, string contrasenia);
  List<UsuarioActiveDirectoryDto> SearchUsersTop10(string? consulta);
}