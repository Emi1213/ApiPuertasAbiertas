namespace ApiPuertasAbiertas.Application.DTOs.Empresa;

using System.ComponentModel.DataAnnotations;

public class CrearEmpresaDto
{
  [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
  [MaxLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
  public required string Nombre { get; set; }
  
  [Required(ErrorMessage = "Debe especificar el estado de la empresa.")]
  public required bool Estado { get; set; }
}
