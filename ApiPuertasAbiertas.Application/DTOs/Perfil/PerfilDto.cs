namespace ApiPuertasAbiertas.Application.DTOs.Perfil;

public class PerfilDto
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public required string Descripcion { get; set; }
  public required bool Estado { get; set; }
}