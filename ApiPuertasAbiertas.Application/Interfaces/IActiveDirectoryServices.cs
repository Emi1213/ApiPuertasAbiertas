using ApiPuertasAbiertas.Application.DTOs.Usuarios;

namespace ApiPuertasAbiertas.Application.Interfaces;

public interface IActiveDirectoryServices
{
  bool ValidateActiveDirectoryLogin(string username, string pwd);
  List<UsuarioActiveDirectoryDto> SearchUsersTop10(string bindUser, string bindPwd, string? query);
}