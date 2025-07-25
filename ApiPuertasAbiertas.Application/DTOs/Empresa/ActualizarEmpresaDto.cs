namespace ApiPuertasAbiertas.Application.DTOs.Empresa;

using System.ComponentModel.DataAnnotations;
public class ActualizarEmpresaDto
{
  [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
  [StringLength(100, ErrorMessage = "El nombre de la empresa no puede exceder los 100 caracteres.")]
  public required string Nombre { get; set; }

  public bool Estado { get; set; } 
}