using System.Security.Claims;
using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Application.UseCases.ModulosPerfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/navegacion/me")]
public class ModuloNavegacionController : ControllerBase
{
  private readonly ModulosNavegacionUseCases _modulosNavegacionUseCases;

  public ModuloNavegacionController(ModulosNavegacionUseCases modulosNavegacionUseCases)
  {
    _modulosNavegacionUseCases = modulosNavegacionUseCases;

  }

  [HttpGet]
  public async Task<ActionResult<ModulosNavegacionDto>> ObtenerModulosPorPerfilActual()
  {
    var userPerfilId = User.FindFirst(ClaimTypes.Role)?.Value;
    if (string.IsNullOrEmpty(userPerfilId) || !int.TryParse(userPerfilId, out int perfilId))
    {
      return BadRequest("Token de usuario inválido");
    }

    var modulos = await _modulosNavegacionUseCases.ObtenerModulosNavegacionAsync(perfilId);
    return Ok(modulos);
  }


}
