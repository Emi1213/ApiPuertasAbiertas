using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Application.UseCases.Empresas;
using ApiPuertasAbiertas.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/empresas")]
public class EmpresaController : ControllerBase
{
  private readonly EmpresaUseCases _empresaUseCases;
  private readonly BuscarEmpresasUseCase _buscarEmpresasUseCase;

  public EmpresaController(EmpresaUseCases empresaUseCases, BuscarEmpresasUseCase buscarEmpresasUseCase)
  {
    _empresaUseCases = empresaUseCases;
    _buscarEmpresasUseCase = buscarEmpresasUseCase;
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
  [HttpGet("buscar")]
  public async Task<object> BuscarConFiltros([FromQuery] BuscarEmpresasQuery query)
  {
    var resultado = await _buscarEmpresasUseCase.ExecuteAsync(query);
    return Results.Ok(resultado);
  }
  [HttpPost]
  public async Task<object> Crear([FromBody] CrearEmpresaDto dto)
  {
    var empresaCreada = await _empresaUseCases.CrearAsync(dto);
    return Results.Ok(empresaCreada);
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