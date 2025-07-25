namespace ApiPuertasAbiertas.Application.DTOs.Auth;

public class LoginDto
{
  public required string Usuario { get; set; }
  public required string Contrasena { get; set; }
}
