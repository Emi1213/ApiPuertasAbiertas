using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Modulos;

public class BuscarModulosUseCases
{
  private readonly IModuloRepository _repository;
  private readonly IMapper _mapper;

  public BuscarModulosUseCases(IModuloRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<RespuestaPaginada<ModuloDto>> ExecuteAsync(BuscarModulosQuery query)
  {
    (int total, List<Modulo> modulos) = await _repository.BuscarAsync(
      query.busqueda,
      query.pagina,
      query.tamanioPagina
    );

    var items = _mapper.Map<List<ModuloDto>>(modulos);

    return new RespuestaPaginada<ModuloDto>
    {
      Items = items,
      TotalItems = total,
      TotalPaginas = (int)Math.Ceiling((double)total / query.tamanioPagina),
      Pagina = query.pagina,
      TamanioPagina = query.tamanioPagina
    };
  }
}