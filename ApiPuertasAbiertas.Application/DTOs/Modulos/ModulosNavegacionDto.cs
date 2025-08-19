namespace ApiPuertasAbiertas.Application.DTOs.Modulos;

public class ModulosNavegacionDto
{
  public int PerfilId { get; set; }
  public required int RbacVersion { get; set; }
  public required List<ModuloDto> Modulos { get; set; }
}