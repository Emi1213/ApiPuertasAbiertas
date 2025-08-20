using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Application.UseCases.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
  private readonly UsuarioUseCases _usuarioUseCases;
  private readonly BuscarUsuariosUseCases _buscarUsuariosUseCases;
  private readonly BuscarUsuariosActiveDirectoryUseCase _buscarUsuariosActiveDirectoryUseCase;

  public UsuarioController(
    UsuarioUseCases usuarioUseCases,
    BuscarUsuariosUseCases buscarUsuariosUseCases,
    BuscarUsuariosActiveDirectoryUseCase buscarUsuariosActiveDirectoryUseCase)
  {
    _usuarioUseCases = usuarioUseCases;
    _buscarUsuariosUseCases = buscarUsuariosUseCases;
    _buscarUsuariosActiveDirectoryUseCase = buscarUsuariosActiveDirectoryUseCase;
  }

  [HttpGet]
  public async Task<object> ObtenerTodos()
  {
    var usuarios = await _usuarioUseCases.ObtenerTodosAsync();
    return Results.Ok(usuarios);
  }

  [HttpGet("{id}")]
  public async Task<object> ObtenerPorId(int id)
  {
    var usuario = await _usuarioUseCases.ObtenerPorIdAsync(id);
    if (usuario == null)
      throw new KeyNotFoundException("Usuario no encontrado.");
    return Results.Ok(usuario);
  }
  [HttpGet("buscar")]
  public async Task<object> BuscarConFiltros([FromQuery] BuscarUsuariosQuery query)
  {
    var resultado = await _buscarUsuariosUseCases.ExecuteAsync(query);
    return Results.Ok(resultado);
  }

  [HttpGet("buscar-active-directory")]
  public async Task<object> BuscarEnActiveDirectory(
    [FromQuery] BusquedaActiveDirectoryRequestDto request)
  {
    var usuarios = await _buscarUsuariosActiveDirectoryUseCase.ExecuteAsync(request, User);
    return Results.Ok(usuarios);
  }

  [HttpPost]
  public async Task<object> Crear([FromBody] CrearUsuarioDto dto)
  {
    await _usuarioUseCases.CrearAsync(dto);
    return Results.Ok("Usuario creado exitosamente.");
  }

  [HttpPut("{id}")]
  public async Task<object> Actualizar(int id, [FromBody] ActualizarUsuarioDto dto)
  {
    if (id != dto.Id) return Results.BadRequest("El ID del usuario no coincide.");
    await _usuarioUseCases.ActualizarAsync(dto);
    return Results.Ok("Usuario actualizado exitosamente.");

  }

  [HttpDelete("{id}")]
  public async Task<object> Eliminar(int id)
  {
    await _usuarioUseCases.EliminarAsync(id);
    return Results.Ok("Usuario eliminado exitosamente.");
  }
}