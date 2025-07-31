using System.ComponentModel.DataAnnotations;

namespace ApiPuertasAbiertas.Application.DTOs.Empresa;

public class EmpresaDto
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public required bool Estado { get; set; }
}
