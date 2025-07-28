namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class CrearUsuarioDto
{
  public required string Usuario { get; set; }
  public required string Nombre { get; set; }
  public string? Descripcion { get; set; }
  public required string Contrasenia { get; set; }
  public required int PerfilId { get; set; }
}