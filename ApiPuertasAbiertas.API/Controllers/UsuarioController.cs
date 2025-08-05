using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Application.DTOs.Usuarios;
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

  public UsuarioController(UsuarioUseCases usuarioUseCases, BuscarUsuariosUseCases buscarUsuariosUseCases)
  {
    _usuarioUseCases = usuarioUseCases;
    _buscarUsuariosUseCases = buscarUsuariosUseCases;
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

    try
    {
      await _usuarioUseCases.ActualizarAsync(dto);
      return Results.Ok("Usuario actualizado exitosamente.");
    }
    catch (KeyNotFoundException)
    {
      return Results.NotFound();
    }
  }

  [HttpDelete("{id}")]
  public async Task<object> Eliminar(int id)
  {
    await _usuarioUseCases.EliminarAsync(id);
    return Results.Ok("Usuario eliminado exitosamente.");
  }
}