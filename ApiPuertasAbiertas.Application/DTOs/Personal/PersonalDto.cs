namespace ApiPuertasAbiertas.Application.DTOs.Personal
{
  public class PersonalDto
  {
    public int Id { get; set; }
    public required string Nombres { get; set; }
    public required string Apellidos { get; set; }
    public required bool Estado { get; set; }
  }
}