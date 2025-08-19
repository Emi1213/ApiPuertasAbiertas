using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Interfaces;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.DTOs.ModulosPerfil;

public class ModulosPerfilUseCases
{
  private readonly IModuloPerfilRepository _moduloPerfilRepository;
  private readonly IMapper _mapper;
  private readonly IRbacNotifier _notifier;

  public ModulosPerfilUseCases(
        IModuloPerfilRepository moduloPerfilRepository,
        IMapper mapper,
        IRbacNotifier notifier)
  {
    _moduloPerfilRepository = moduloPerfilRepository;
    _mapper = mapper;
    _notifier = notifier;
  }

  public async Task<List<ModuloDto>> ObtenerPorPerfilAsync(int perfilId)
  {
    var modulos = await _moduloPerfilRepository.ObtenerPorPerfilAsync(perfilId);
    return _mapper.Map<List<ModuloDto>>(modulos);
  }

  public async Task AsignarModulosAsync(int perfilId, List<int> modulosIds)
  {
    await _moduloPerfilRepository.EliminarPorPerfilAsync(perfilId);
    if (modulosIds.Any())
    {
      await _moduloPerfilRepository.AsignarModulosAsync(perfilId, modulosIds);
    }
    await _notifier.NotificarCambioModulosAsync(perfilId);
  }

  public async Task ActualizarModulosAsync(int perfilId, List<int> modulosIds)
  {
    await AsignarModulosAsync(perfilId, modulosIds);
  }
}