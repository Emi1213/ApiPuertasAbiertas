using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Application.DTOs.ModulosPerfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/perfiles/{perfilId}/modulos")]
public class ModuloPerfilController : ControllerBase
{
  private readonly ModulosPerfilUseCases _modulosPerfilUseCases;

  public ModuloPerfilController(ModulosPerfilUseCases modulosPerfilUseCases)
  {
    _modulosPerfilUseCases = modulosPerfilUseCases;
  }

  [HttpGet]
  public async Task<ActionResult<List<ModuloDto>>> ObtenerModulosPorPerfil(int perfilId)
  {
    var modulos = await _modulosPerfilUseCases.ObtenerPorPerfilAsync(perfilId);
    return Ok(modulos);
  }

  [HttpPost]
  public async Task<ActionResult> AsignarModulos(int perfilId, [FromBody] List<int> modulosIds)
  {
    await _modulosPerfilUseCases.AsignarModulosAsync(perfilId, modulosIds);
    return Ok("Módulos asignados exitosamente al perfil.");
  }

  [HttpPut]
  public async Task<ActionResult> ActualizarModulos(int perfilId, [FromBody] List<int> modulosIds)
  {
    await _modulosPerfilUseCases.ActualizarModulosAsync(perfilId, modulosIds);
    return Ok("Módulos del perfil actualizados exitosamente.");
  }
}
