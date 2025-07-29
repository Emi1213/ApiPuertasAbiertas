namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class ActualizarUsuarioDto
{
  public int Id { get; set; }
  public string? Usuario { get; set; }
  public string? Nombre { get; set; }
  public string? Descripcion { get; set; }
  public int PerfilId { get; set; }
}