namespace ApiPuertasAbiertas.Application.DTOs.Perfil;

public class CrearPerfilDto
{
  public required string Nombre { get; set; }
  public string? Descripcion { get; set; }
  public required bool Estado { get; set; }
}
