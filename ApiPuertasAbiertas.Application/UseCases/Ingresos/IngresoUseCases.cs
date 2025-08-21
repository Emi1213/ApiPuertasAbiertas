using ApiPuertasAbiertas.Application.DTOs.Ingresos;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Domain.Enums;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Ingresos;

public class IngresoUseCases
{
  private readonly IIngresoRepository _ingresoRepository;
  private readonly IMapper _mapper;
  private readonly IClock _clock;

  public IngresoUseCases(IIngresoRepository ingresoRepository, IMapper mapper, IClock clock)
  {
    _ingresoRepository = ingresoRepository;
    _mapper = mapper;
    _clock = clock;
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

  public async Task<IngresoDto> CrearAsync(CrearIngresoDto crearIngresoDto, int usuarioId)
  {
    var ingreso = _mapper.Map<Domain.Entities.Ingreso>(crearIngresoDto);

    ingreso.UsuarioReconId = usuarioId;
    ingreso.FechaRecon = _clock.Now;

    if (ingreso.FechaFin.HasValue)
    {
      var duracion = ingreso.FechaFin.Value - ingreso.FechaInicio;
      ingreso.Duracion = $"{(int)duracion.TotalHours:D2}:{duracion.Minutes:D2}";
      ingreso.Estado = EstadoIngreso.Cerrado;
    }
    else
    {
      ingreso.Duracion = null;
      ingreso.Estado = EstadoIngreso.EnProceso;
    }

    await _ingresoRepository.CrearAsync(ingreso);

    var ingresoCreado = await _ingresoRepository.ObtenerPorIdAsync(ingreso.Id);
    return _mapper.Map<IngresoDto>(ingresoCreado);
  }

  public async Task ActualizarAsync(ActualizarIngresoDto actualizarIngresoDto)
  {
    var ingresoExistente = await _ingresoRepository.ObtenerPorIdAsync(actualizarIngresoDto.Id);
    if (ingresoExistente == null)
    {
      throw new KeyNotFoundException("Ingreso no encontrado");
    }

    _mapper.Map(actualizarIngresoDto, ingresoExistente);

    if (ingresoExistente.FechaFin.HasValue)
    {
      var duracion = ingresoExistente.FechaFin.Value - ingresoExistente.FechaInicio;
      ingresoExistente.Duracion = $"{(int)duracion.TotalHours:D2}:{duracion.Minutes:D2}";
      ingresoExistente.Estado = EstadoIngreso.Cerrado;
    }
    else
    {
      ingresoExistente.Duracion = null;
      ingresoExistente.Estado = EstadoIngreso.EnProceso;
    }

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
