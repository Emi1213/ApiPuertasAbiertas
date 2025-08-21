using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.ModulosPerfil;

public class ModulosPerfilUseCases
{
  private readonly IModuloPerfilRepository _moduloPerfilRepository;
  private readonly IMapper _mapper;

  public ModulosPerfilUseCases(
        IModuloPerfilRepository moduloPerfilRepository,
        IMapper mapper)
  {
    _moduloPerfilRepository = moduloPerfilRepository;
    _mapper = mapper;
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
  }

  public async Task ActualizarModulosAsync(int perfilId, List<int> modulosIds)
  {
    await AsignarModulosAsync(perfilId, modulosIds);
  }
}