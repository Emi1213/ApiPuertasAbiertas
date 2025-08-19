using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.DTOs.ModulosPerfil;

public class ModulosPerfilUseCases
{
  private readonly IModuloPerfilRepository _moduloPerfilRepository;
  private readonly IMapper _mapper;

  public ModulosPerfilUseCases(IModuloPerfilRepository moduloPerfilRepository, IMapper mapper)
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
    // Primero eliminamos todas las asignaciones existentes del perfil
    await _moduloPerfilRepository.EliminarPorPerfilAsync(perfilId);

    // Luego asignamos los nuevos módulos
    if (modulosIds.Any())
    {
      await _moduloPerfilRepository.AsignarModulosAsync(perfilId, modulosIds);
    }
  }

  public async Task ActualizarModulosAsync(int perfilId, List<int> modulosIds)
  {
    // Esta función es la misma que AsignarModulosAsync ya que reemplaza completamente los módulos
    await AsignarModulosAsync(perfilId, modulosIds);
  }
}