using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Perfiles;

public class PerfilUseCases
{
  private readonly IPerfilRepository _perfilRepository;
  private readonly IMapper _mapper;
  public PerfilUseCases(IPerfilRepository perfilRepository, IMapper mapper)
  {
    _perfilRepository = perfilRepository;
    _mapper = mapper;
  }

  public async Task<List<PerfilDto>> ObtenerTodosAsync()
  {
    var perfiles = await _perfilRepository.ObtenerTodosAsync();
    return _mapper.Map<List<PerfilDto>>(perfiles);
  }

}