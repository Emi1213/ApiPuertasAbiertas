using ApiPuertasAbiertas.Application.DTOs.Auth;
using ApiPuertasAbiertas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IServicioAuth _servicioAuth;

  public AuthController(IServicioAuth servicioAuth)
  {
    _servicioAuth = servicioAuth;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginDto dto)
  {
    var result = await _servicioAuth.AutenticarAsync(dto);
    if (result == null)
      return Unauthorized(new { mensaje = "Credenciales inválidas" });

    return Ok(result);
  }

}