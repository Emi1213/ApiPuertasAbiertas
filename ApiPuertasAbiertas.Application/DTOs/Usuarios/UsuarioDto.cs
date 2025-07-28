using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class UsuarioDto
{
  public int Id { get; set; }
  public required string UsuarioNombre { get; set; }
  public required string Nombre { get; set; }
  public required string Descripcion { get; set; }
  public required Perfil Perfil { get; set; }

}