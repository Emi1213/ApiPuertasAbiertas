using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.DTOs.ModulosPerfil;

public class ModulosNavegacionUseCases
{
  private readonly IModuloPerfilRepository _moduloPerfilRepository;
  private readonly IPerfilRepository _perfilRepository;
  private readonly IMapper _mapper;
  public ModulosNavegacionUseCases(IModuloPerfilRepository moduloPerfilRepository, IPerfilRepository perfilRepository, IMapper mapper)
  {
    _moduloPerfilRepository = moduloPerfilRepository;
    _perfilRepository = perfilRepository;
    _mapper = mapper;
  }

  public async Task<ModulosNavegacionDto> ObtenerModulosNavegacionAsync(int perfilId)
  {
    var modulos = await _moduloPerfilRepository.ObtenerPorPerfilAsync(perfilId);
    var perfil = await _perfilRepository.ObtenerPorIdAsync(perfilId);

    return new ModulosNavegacionDto
    {
      PerfilId = perfilId,
      Modulos = _mapper.Map<List<ModuloDto>>(modulos),
      RbacVersion = perfil?.RbacVersion ?? 0
    };
  }
}