using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Application.UseCases.Modulos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/modulos")]
public class ModuloController : ControllerBase
{
  private readonly ModuloUseCases _moduloUseCases;
  private readonly BuscarModulosUseCases _buscarModulosUseCases;

  public ModuloController(ModuloUseCases moduloUseCases, BuscarModulosUseCases buscarModulosUseCases)
  {
    _moduloUseCases = moduloUseCases;
    _buscarModulosUseCases = buscarModulosUseCases;
  }

  [HttpGet("buscar")]
  public async Task<object> Buscar([FromQuery] BuscarModulosQuery query)
  {
    var resultado = await _buscarModulosUseCases.ExecuteAsync(query);
    return Results.Ok(resultado);
  }

  [HttpGet]
  public async Task<object> ObtenerTodos()
  {
    var modulos = await _moduloUseCases.ObtenerTodosAsync();
    return Results.Ok(modulos);
  }

  [HttpGet("{id}")]
  public async Task<object> ObtenerPorId(int id)
  {
    var modulo = await _moduloUseCases.ObtenerPorIdAsync(id);
    if (modulo == null)
    {
      return Results.NotFound("Modulo no encontrado");
    }
    return Results.Ok(modulo);
  }
  [HttpPost]
  public async Task<object> Crear([FromBody] CrearModuloDto dto)
  {
    var resultado = await _moduloUseCases.CrearAsync(dto);
    return Results.Ok(resultado);
  }
  [HttpDelete("{id}")]
  public async Task<object> Eliminar(int id)
  {
    await _moduloUseCases.EliminarAsync(id);
    return Results.Ok("Modulo eliminado exitosamente");
  }
}