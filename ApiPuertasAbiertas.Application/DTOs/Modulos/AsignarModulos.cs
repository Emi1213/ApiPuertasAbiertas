namespace ApiPuertasAbiertas.Application.DTOs.Modulos;

public class AsignarModulos
{
  public int UsuarioId { get; set; }
  public required List<int> ModuloIds { get; set; }
}
