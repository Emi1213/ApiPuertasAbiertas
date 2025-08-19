using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Application.DTOs.Perfiles;
using ApiPuertasAbiertas.Application.UseCases.Perfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/perfiles")]
public class PerfilController : ControllerBase
{
  private readonly PerfilUseCases _perfilUseCases;
  private readonly BuscarPerfilesUseCases _buscarPerfilesUseCases;

  public PerfilController(PerfilUseCases perfilUseCases, BuscarPerfilesUseCases buscarPerfilesUseCases)
  {
    _perfilUseCases = perfilUseCases;
    _buscarPerfilesUseCases = buscarPerfilesUseCases;
  }

  [HttpGet]
  public async Task<object> ObtenerTodos()
  {
    var perfiles = await _perfilUseCases.ObtenerTodosAsync();
    return Results.Ok(perfiles);
  }

  [HttpGet("buscar")]
  public async Task<object> BuscarConFiltros([FromQuery] BuscarPerfilesQuery query)
  {
    var resultado = await _buscarPerfilesUseCases.ExecuteAsync(query);
    return Results.Ok(resultado);
  }

  [HttpGet("{id}")]
  public async Task<object> ObtenerPorId(int id)
  {
    var perfil = await _perfilUseCases.ObtenerPorIdAsync(id);
    if (perfil == null)
    {
      return Results.NotFound("Perfil no encontrado");
    }
    return Results.Ok(perfil);
  }

  [HttpPost]
  public async Task<object> Crear([FromBody] CrearPerfilDto dto)
  {
    var perfil = await _perfilUseCases.CrearAsync(dto);
    return Results.Ok(perfil);
  }

  [HttpPut("{id}")]
  public async Task<object> Actualizar(int id, [FromBody] ActualizarPerfilDto dto)
  {
    dto.Id = id;
    await _perfilUseCases.ActualizarAsync(dto);
    return Results.Ok("Perfil actualizado exitosamente");
  }

  [HttpDelete("{id}")]
  public async Task<object> Eliminar(int id)
  {
    await _perfilUseCases.EliminarAsync(id);
    return Results.Ok("Perfil eliminado exitosamente");
  }
}