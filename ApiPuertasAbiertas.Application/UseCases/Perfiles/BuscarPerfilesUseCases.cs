using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Application.DTOs.Perfiles;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Perfiles;

public class BuscarPerfilesUseCases
{
  private readonly IPerfilRepository _perfilRepository;
  private readonly IMapper _mapper;

  public BuscarPerfilesUseCases(IPerfilRepository perfilRepository, IMapper mapper)
  {
    _perfilRepository = perfilRepository;
    _mapper = mapper;
  }

  public async Task<RespuestaPaginada<PerfilDto>> ExecuteAsync(BuscarPerfilesQuery query)
  {
    (int total, List<Perfil> perfiles) = await _perfilRepository.BuscarAsync(
        query.busqueda,
        query.pagina,
        query.tamanioPagina);

    var items = _mapper.Map<List<PerfilDto>>(perfiles);

    return new RespuestaPaginada<PerfilDto>
    {
      Items = items,
      TotalItems = total,
      TotalPaginas = (int)Math.Ceiling((double)total / query.tamanioPagina),
      Pagina = query.pagina,
      TamanioPagina = query.tamanioPagina
    };
  }
}
