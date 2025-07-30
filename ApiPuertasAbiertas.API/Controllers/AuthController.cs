using ApiPuertasAbiertas.Application.DTOs.Auth;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Application.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly LoginUseCase _loginUseCase;

  public AuthController(LoginUseCase loginUseCase)
  {
    _loginUseCase = loginUseCase;
  }

  [HttpPost("login")]
  public async Task<object> Login([FromBody] LoginDto dto)
  {
    var resultado = await _loginUseCase.ExecuteAsync(dto.Usuario, dto.Contrasenia);
    if (resultado == null)
      return Results.BadRequest("Credenciales inválidas");

    return Results.Ok(resultado);
  }

}