namespace ApiPuertasAbiertas.Application.DTOs.Perfil;

public class ActualizarPerfilDto
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public string? Descripcion { get; set; }
  public List<int> ModulosIds { get; set; } = new List<int>();
}
