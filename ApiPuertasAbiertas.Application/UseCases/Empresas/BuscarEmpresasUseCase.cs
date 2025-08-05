using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Empresas;

public class BuscarEmpresasUseCase
{
  private readonly IEmpresaRepository _repository;
  private readonly IMapper _mapper;

  public BuscarEmpresasUseCase(IEmpresaRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<RespuestaPaginada<EmpresaDto>> ExecuteAsync(BuscarEmpresasQuery query)
  {
    (int total, List<Empresa> empresas) = await _repository.BuscarAsync(
      query.busqueda,
      query.estado,
      query.pagina,
      query.tamanioPagina
    );

    var items = _mapper.Map<List<EmpresaDto>>(empresas);

    return new RespuestaPaginada<EmpresaDto>
    {
      Items = items,
      TotalItems = total,
      TotalPaginas = (int)Math.Ceiling((double)total / query.tamanioPagina),
      Pagina = query.pagina,
      TamanioPagina = query.tamanioPagina
    };
  }

}
