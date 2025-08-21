namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class UsuarioActiveDirectoryDto
{
  public string SamAccountName { get; set; } = "";
  public string NombreParaMostrar { get; set; } = "";
  public string UsuarioNombre { get; set; } = "";
  public string? Correo { get; set; }
}