namespace ApiPuertasAbiertas.Domain.Entities;

public class Usuario
{
  public int Id { get; set; }
  public required string NombreUsuario { get; set; }
  public required string Nombre { get; set; }
  public string? Descripcion { get; set; }
  public required string Contrasenia { get; set; }
  public int PerfilId { get; set; }
  public required Perfil Perfil { get; set; }
}