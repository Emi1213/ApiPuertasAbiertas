namespace ApiPuertasAbiertas.Application.DTOs.Personal
{
  public class CrearPersonalDto
  {
    public required string Nombres { get; set; }
    public required string Apellidos { get; set; }
    public required bool Estado { get; set; }
    public int EmpresaId { get; set; }
  }
}