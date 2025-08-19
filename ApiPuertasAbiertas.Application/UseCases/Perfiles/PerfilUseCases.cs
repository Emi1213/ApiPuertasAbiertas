using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Interfaces;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Perfiles;

public class PerfilUseCases
{
  private readonly IPerfilRepository _perfilRepository;
  private readonly IModuloPerfilRepository _moduloPerfilRepository;
  private readonly IRbacNotifier _notifier;
  private readonly IMapper _mapper;

  public PerfilUseCases(
    IPerfilRepository perfilRepository,
    IModuloPerfilRepository moduloPerfilRepository,
    IRbacNotifier notifier,
    IMapper mapper)
  {
    _perfilRepository = perfilRepository;
    _moduloPerfilRepository = moduloPerfilRepository;
    _notifier = notifier;
    _mapper = mapper;
  }

  public async Task<List<PerfilDto>> ObtenerTodosAsync()
  {
    var perfiles = await _perfilRepository.ObtenerTodosAsync();
    return _mapper.Map<List<PerfilDto>>(perfiles);
  }

  public async Task<PerfilDto?> ObtenerPorIdAsync(int id)
  {
    var perfil = await _perfilRepository.ObtenerPorIdAsync(id);
    return perfil == null ? null : _mapper.Map<PerfilDto>(perfil);
  }

  public async Task<PerfilDto> CrearAsync(CrearPerfilDto crearPerfilDto)
  {
    var perfil = _mapper.Map<Domain.Entities.Perfil>(crearPerfilDto);
    await _perfilRepository.CrearAsync(perfil);

    // Asignar módulos si se proporcionaron
    if (crearPerfilDto.ModulosIds.Any())
    {
      await _moduloPerfilRepository.AsignarModulosAsync(perfil.Id, crearPerfilDto.ModulosIds);
    }

    // Obtener el perfil creado con módulos
    var perfilCreado = await _perfilRepository.ObtenerPorIdAsync(perfil.Id);
    return _mapper.Map<PerfilDto>(perfilCreado);
  }

  public async Task ActualizarAsync(ActualizarPerfilDto actualizarPerfilDto)
  {
    var perfilExistente = await _perfilRepository.ObtenerPorIdAsync(actualizarPerfilDto.Id);
    if (perfilExistente == null)
    {
      throw new KeyNotFoundException("Perfil no encontrado");
    }

    _mapper.Map(actualizarPerfilDto, perfilExistente);
    await _perfilRepository.ActualizarAsync(perfilExistente);

    // Actualizar módulos
    await _moduloPerfilRepository.EliminarPorPerfilAsync(actualizarPerfilDto.Id);
    if (actualizarPerfilDto.ModulosIds.Any())
    {
      await _moduloPerfilRepository.AsignarModulosAsync(actualizarPerfilDto.Id, actualizarPerfilDto.ModulosIds);
    }

    // Notificar cambios a usuarios con este perfil
    await _notifier.NotificarCambioModulosAsync(actualizarPerfilDto.Id);
  }

  public async Task EliminarAsync(int id)
  {
    var perfil = await _perfilRepository.ObtenerPorIdAsync(id);
    if (perfil == null)
    {
      throw new KeyNotFoundException("Perfil no encontrado");
    }

    // Eliminar módulos asociados
    await _moduloPerfilRepository.EliminarPorPerfilAsync(id);
    await _perfilRepository.EliminarAsync(id);

    // Notificar cambios
    await _notifier.NotificarCambioModulosAsync(id);
  }
}