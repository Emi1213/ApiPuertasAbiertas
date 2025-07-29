using ApiPuertasAbiertas.Application.DTOs.Perfil;

namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class UsuarioDto
{
  public int Id { get; set; }
  public required string Usuario { get; set; }
  public required string Nombre { get; set; }
  public required string Descripcion { get; set; }
  public required PerfilDto Perfil { get; set; }

}