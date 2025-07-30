using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Application.UseCases.Empresas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
  private readonly EmpresaUseCases _empresaUseCases;

  public EmpresaController(EmpresaUseCases empresaUseCases)
  {
    _empresaUseCases = empresaUseCases;
  }
  [HttpGet]
  public async Task<object> ObtenerTodos()
  {
    var empresas = await _empresaUseCases.ObtenerTodosAsync();
    return Ok(empresas);
  }
  [HttpGet("{id}")]
  public async Task<object> ObtenerPorId(int id)
  {
    var empresa = await _empresaUseCases.ObtenerPorIdAsync(id);
    if (empresa == null)
      return Results.NotFound("Empresa no encontrada");
    return Results.Ok(empresa);
  }
  [HttpPost]
  public async Task<object> Crear([FromBody] CrearEmpresaDto dto)
  {
    await _empresaUseCases.CrearAsync(dto);
    return Results.Ok("Empresa creada exitosamente.");
  }
  [HttpPut("{id}")]
  public async Task<object> Actualizar(int id, [FromBody] EmpresaDto dto)
  {
    dto.Id = id;
    await _empresaUseCases.ActualizarAsync(dto);
    return Results.Ok("Empresa actualizada exitosamente.");
  }
  [HttpDelete("{id}")]
  public async Task<object> Eliminar(int id)
  {
    await _empresaUseCases.EliminarAsync(id);
    return Results.Ok("Empresa eliminada exitosamente.");
  }
  [HttpGet("buscar-por-nombre")]
  public async Task<object> BuscarPorNombre(string nombre)
  {
    var empresas = await _empresaUseCases.BuscarPorNombreAsync(nombre);
    return Results.Ok(empresas);
  }
}