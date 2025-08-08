using ApiPuertasAbiertas.Application.DTOs.Ingresos;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Ingresos;

public class IngresoUseCases
{
  private readonly IIngresoRepository _ingresoRepository;
  private readonly IMapper _mapper;

  public IngresoUseCases(IIngresoRepository ingresoRepository, IMapper mapper)
  {
    _ingresoRepository = ingresoRepository;
    _mapper = mapper;
  }

  public async Task<List<IngresoDto>> ObtenerTodosAsync()
  {
    var ingresos = await _ingresoRepository.ObtenerTodosAsync();
    return _mapper.Map<List<IngresoDto>>(ingresos);
  }

  public async Task<IngresoDto?> ObtenerPorIdAsync(int id)
  {
    var ingreso = await _ingresoRepository.ObtenerPorIdAsync(id);
    return ingreso == null ? null : _mapper.Map<IngresoDto>(ingreso);
  }

  public async Task<List<IngresoDto>> ObtenerPorPersonalIdAsync(int personalId)
  {
    var ingresos = await _ingresoRepository.ObtenerPorPersonalIdAsync(personalId);
    return _mapper.Map<List<IngresoDto>>(ingresos);
  }

  public async Task CrearAsync(CrearIngresoDto crearIngresoDto)
  {
    var ingreso = _mapper.Map<Domain.Entities.Ingreso>(crearIngresoDto);
    await _ingresoRepository.CrearAsync(ingreso);
  }

  public async Task ActualizarAsync(ActualizarIngresoDto actualizarIngresoDto)
  {
    var ingresoExistente = await _ingresoRepository.ObtenerPorIdAsync(actualizarIngresoDto.Id);
    if (ingresoExistente == null)
    {
      throw new KeyNotFoundException("Ingreso no encontrado");
    }

    // Mapear los cambios sobre la entidad existente (que ya está trackeada)
    _mapper.Map(actualizarIngresoDto, ingresoExistente);
    await _ingresoRepository.ActualizarAsync(ingresoExistente);
  }

  public async Task EliminarAsync(int id)
  {
    var ingreso = await _ingresoRepository.ObtenerPorIdAsync(id);
    if (ingreso == null)
    {
      throw new KeyNotFoundException("Ingreso no encontrado");
    }
    await _ingresoRepository.EliminarAsync(id);
  }
}
