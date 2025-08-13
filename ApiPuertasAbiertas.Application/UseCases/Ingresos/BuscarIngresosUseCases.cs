using ApiPuertasAbiertas.Application.DTOs.Ingresos;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Ingresos;

public class BuscarIngresosUseCases
{
  private readonly IIngresoRepository _ingresoRepository;
  private readonly IMapper _mapper;

  public BuscarIngresosUseCases(IIngresoRepository ingresoRepository, IMapper mapper)
  {
    _ingresoRepository = ingresoRepository;
    _mapper = mapper;
  }

  public async Task<RespuestaPaginada<IngresoDto>> ExecuteAsync(BuscarIngresosQuery query)
  {
    (int total, List<Ingreso> ingresos) = await _ingresoRepository.BuscarAsync(
      query.busqueda,
      query.estado,
      query.pagina,
      query.tamanioPagina);

    var items = _mapper.Map<List<IngresoDto>>(ingresos);

    return new RespuestaPaginada<IngresoDto>
    {
      Items = items,
      TotalItems = total,
      TotalPaginas = (int)Math.Ceiling((double)total / query.tamanioPagina),
      TamanioPagina = query.tamanioPagina
    };

  }
}
