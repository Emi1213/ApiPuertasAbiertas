using ApiPuertasAbiertas.Application.DTOs.Modulos;

namespace ApiPuertasAbiertas.Application.DTOs.Perfil;

public class PerfilDto
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public required string Descripcion { get; set; }
  public bool Estado { get; set; }
  public List<ModuloDto> Modulos { get; set; } = new List<ModuloDto>();
}