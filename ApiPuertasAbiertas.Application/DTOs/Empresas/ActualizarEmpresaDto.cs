namespace ApiPuertasAbiertas.Application.DTOs.Empres;

using System.ComponentModel.DataAnnotations;
public class ActualizarEmpresaDto
{

  public string? Nombre { get; set; }

  public bool? Estado { get; set; }
}