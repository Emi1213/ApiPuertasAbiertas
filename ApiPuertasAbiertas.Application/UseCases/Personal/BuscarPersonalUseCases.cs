
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;
using AutoMapper;
using ApiPuertasAbiertas.Application.DTOs.Personal;
using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Application.UseCases.Personal;

public class BuscarPersonalUseCases
{
  private readonly IPersonalRepository _repository;
  private readonly IMapper _mapper;
  public BuscarPersonalUseCases(IPersonalRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<RespuestaPaginada<PersonalDto>> ExecuteAsync(BuscarPersonalQuery query)
  {
    (int total, List<ApiPuertasAbiertas.Domain.Entities.Personal> personal) = await _repository.BuscarAsync(
      query.busqueda,
      query.estado,
      query.pagina,
      query.tamanioPagina
    );
    var items = _mapper.Map<List<PersonalDto>>(personal);

    return new RespuestaPaginada<PersonalDto>
    {
      Items = items,
      TotalItems = total,
      TotalPaginas = (int)Math.Ceiling((double)total / query.tamanioPagina),
      Pagina = query.pagina,
      TamanioPagina = query.tamanioPagina
    };
  }

}