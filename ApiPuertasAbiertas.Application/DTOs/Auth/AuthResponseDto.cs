namespace ApiPuertasAbiertas.Application.DTOs.Auth;

public class AuthResponseDto
{
  public string Token { get; set; } = null!;
  public DateTime Expiration { get; set; }
  public string Usuario { get; set; } = null!;
}