namespace ApiPuertasAbiertas.Application.DTOs.Auth;

public class AuthResponseDto
{
  public required string Token { get; set; }
  public DateTime Expiracion { get; set; }
}