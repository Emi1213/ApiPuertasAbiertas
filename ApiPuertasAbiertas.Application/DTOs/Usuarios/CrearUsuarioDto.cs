using System.ComponentModel.DataAnnotations;

namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class CrearUsuarioDto
{
  [Required(ErrorMessage = "El usuario es obligatorio.")]
  public required string Usuario { get; set; }
  [Required(ErrorMessage = "El nombre es obligatorio.")]
  public required string Nombre { get; set; }
  public string? Descripcion { get; set; }
  [Required(ErrorMessage = "La contraseña es obligatoria.")]
  public required string Contrasenia { get; set; }
  [Required(ErrorMessage = "El perfil es obligatorio.")]
  public required int PerfilId { get; set; }
}