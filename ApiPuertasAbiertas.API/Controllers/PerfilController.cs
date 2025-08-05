using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Application.UseCases.Perfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/perfiles")]
public class PerfilController : ControllerBase
{
  private readonly PerfilUseCases _perfilUseCases;
  public PerfilController(PerfilUseCases perfilUseCases)
  {
    _perfilUseCases = perfilUseCases;
  }

  [HttpGet]
  public async Task<List<PerfilDto>> ObtenerTodosAsync()
  {
    return await _perfilUseCases.ObtenerTodosAsync();
  }
}