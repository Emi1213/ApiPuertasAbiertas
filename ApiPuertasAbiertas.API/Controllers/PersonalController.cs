using ApiPuertasAbiertas.Application.DTOs.Personal;
using ApiPuertasAbiertas.Application.UseCases.Personal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/personal")]
public class PersonalController : ControllerBase
{
  private readonly PersonalUseCases _personalUseCases;
  private readonly BuscarPersonalUseCases _buscarPersonalUseCases;
  public PersonalController(PersonalUseCases personalUseCases, BuscarPersonalUseCases buscarPersonalUseCases)
  {
    _personalUseCases = personalUseCases;
    _buscarPersonalUseCases = buscarPersonalUseCases;
  }
  [HttpGet]
  public async Task<object> ObtenerTodos()
  {
    var personal = await _personalUseCases.ObtenerTodosAsync();
    return Results.Ok(personal);
  }

  [HttpGet("{id}")]
  public async Task<object> ObtenerPorId(int id)
  {
    var personal = await _personalUseCases.ObtenerPorIdAsync(id);
    if (personal == null)
    {
      return new KeyNotFoundException("Personal no encontrado.");
    }
    return Results.Ok(personal);
  }

  [HttpGet("buscar")]
  public async Task<object> BuscarConFiltros([FromQuery] BuscarPersonalQuery query)
  {
    var resultado = await _buscarPersonalUseCases.ExecuteAsync(query);
    return Results.Ok(resultado);
  }

  [HttpPost]
  public async Task<object> Crear([FromBody] CrearPersonalDto dto)
  {
    await _personalUseCases.CrearAsync(dto);
    return Results.Ok("Personal creado exitosamente.");
  }

  [HttpPut("{id}")]
  public async Task<object> Actualizar(int id, [FromBody] ActualizarPersonalDto dto)
  {
    dto.Id = id;
    await _personalUseCases.ActualizarAsync(dto);
    return Results.Ok("Personal actualizado exitosamente.");
  }

  [HttpDelete("{id}")]
  public async Task<object> Eliminar(int id)
  {
    await _personalUseCases.EliminarAsync(id);
    return Results.Ok("Personal eliminado exitosamente.");
  }
}