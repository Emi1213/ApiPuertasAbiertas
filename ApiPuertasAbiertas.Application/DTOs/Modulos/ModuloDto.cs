namespace ApiPuertasAbiertas.Application.DTOs.Modulos;

public class ModuloDto
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public required string Alias { get; set; }
}