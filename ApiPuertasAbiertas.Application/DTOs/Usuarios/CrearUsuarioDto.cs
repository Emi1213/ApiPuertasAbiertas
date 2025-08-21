using System.ComponentModel.DataAnnotations;

namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class CrearUsuarioDto
{
  [Required(ErrorMessage = "El usuario es obligatorio.")]
  public required string Usuario { get; set; }
  [Required(ErrorMessage = "El nombre es obligatorio.")]
  public required string Nombre { get; set; }
  public string? Descripcion { get; set; }
  public int? PerfilId { get; set; }
}