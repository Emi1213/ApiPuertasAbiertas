namespace ApiPuertasAbiertas.Domain.Entities;

public class Usuario
{
  public int Id { get; set; }
  public string NombreUsuario { get; set; } = null!;
  public string Nombre { get; set; } = null!;
  public string? Descripcion { get; set; }
  public string Contrasenia { get; set; } = null!;
  public int PerfilId { get; set; }
  public Perfil Perfil { get; set; } = null!;
}