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
  public PersonalController(PersonalUseCases personalUseCases)
  {
    _personalUseCases = personalUseCases;
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

  [HttpPost]
  public async Task<object> Crear([FromBody] CrearPersonalDto dto)
  {
    await _personalUseCases.CrearAsync(dto);
    return Results.Ok("Personal creado exitosamente.");
  }

  [HttpPut]
  public async Task<object> Actualizar([FromBody] ActualizarPersonalDto dto)
  {
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