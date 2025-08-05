namespace ApiPuertasAbiertas.Application.DTOs.Auth;

public class LoginResponseDto
{
  public required string Token { get; set; }
  public DateTime Expiracion { get; set; }
}