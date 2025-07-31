using ApiPuertasAbiertas.Application.DTOs.Personal;
using ApiPuertasAbiertas.Application.UseCases.Personal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonalController : ControllerBase
{
  private readonly PersonalUseCases _personalUseCases;
  public PersonalController(PersonalUseCases personalUseCases)
  {
    _personalUseCases = personalUseCases;
  }
  [HttpGet]
  public async Task<List<PersonalDto>> ObtenerTodosAsync()
  {
    return await _personalUseCases.ObtenerTodosAsync();
  }

  [HttpGet("{id}")]
  public async Task<PersonalDto?> ObtenerPorIdAsync(int id)
  {
    return await _personalUseCases.ObtenerPorIdAsync(id);
  }

  // [HttpPost]
  // public async Task CrearAsync(CrearPersonalDto crearPersonalDto)
  // {
  //   await _personalUseCases.CrearAsync(crearPersonalDto);
  // }

  // [HttpPut]
  // public async Task ActualizarAsync(ActualizarPersonalDto actualizarPersonalDto)
  // {
  //   await _personalUseCases.ActualizarAsync(actualizarPersonalDto);
  // }

  [HttpDelete("{id}")]
  public async Task EliminarAsync(int id)
  {
    await _personalUseCases.EliminarAsync(id);
  }
}