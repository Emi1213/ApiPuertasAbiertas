using ApiPuertasAbiertas.Application.DTOs.Ingresos;
using ApiPuertasAbiertas.Application.UseCases.Ingresos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiPuertasAbiertas.API.Controllers;

[Authorize]
[ApiController]
[Route("api/ingresos")]
public class IngresoController : ControllerBase
{
  private readonly IngresoUseCases _ingresoUseCases;
  private readonly BuscarIngresosUseCases _buscarIngresosUseCases;

  public IngresoController(IngresoUseCases ingresoUseCases, BuscarIngresosUseCases buscarIngresosUseCases)
  {
    _ingresoUseCases = ingresoUseCases;
    _buscarIngresosUseCases = buscarIngresosUseCases;
  }

  [HttpGet]
  public async Task<object> ObtenerTodos()
  {
    var ingresos = await _ingresoUseCases.ObtenerTodosAsync();
    return Results.Ok(ingresos);
  }

  [HttpGet("{id}")]
  public async Task<object> ObtenerPorId(int id)
  {
    var ingreso = await _ingresoUseCases.ObtenerPorIdAsync(id);
    if (ingreso == null)
    {
      return new KeyNotFoundException("Ingreso no encontrado.");
    }
    return Results.Ok(ingreso);
  }

  [HttpGet("personal/{personalId}")]
  public async Task<object> ObtenerPorPersonalId(int personalId)
  {
    var ingresos = await _ingresoUseCases.ObtenerPorPersonalIdAsync(personalId);
    return Results.Ok(ingresos);
  }

  [HttpGet("buscar")]
  public async Task<object> BuscarConFiltros([FromQuery] BuscarIngresosQuery query)
  {
    var resultado = await _buscarIngresosUseCases.ExecuteAsync(query);
    return Results.Ok(resultado);
  }

  [HttpPost]
  public async Task<object> Crear([FromBody] CrearIngresoDto dto)
  {
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int usuarioId))
    {
      return BadRequest("Token de usuario inválido");
    }

    var ingresoCreado = await _ingresoUseCases.CrearAsync(dto, usuarioId);
    return Ok(ingresoCreado);
  }

  [HttpPut("{id}")]
  public async Task<object> Actualizar(int id, [FromBody] ActualizarIngresoDto dto)
  {
    dto.Id = id;
    await _ingresoUseCases.ActualizarAsync(dto);
    return Results.Ok("Ingreso actualizado exitosamente.");
  }

  [HttpDelete("{id}")]
  public async Task<object> Eliminar(int id)
  {
    await _ingresoUseCases.EliminarAsync(id);
    return Results.Ok("Ingreso eliminado exitosamente.");
  }
}
