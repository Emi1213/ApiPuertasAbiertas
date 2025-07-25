namespace ApiPuertasAbiertas.Application.DTOs.Auth;

public class AuthResponseDto
{
  public required string Token { get; set; }
  public DateTime Expiration { get; set; }
  public required string Usuario { get; set; }
}