using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;

namespace ApiPuertasAbiertas.Application.UseCases.Empresas;

public class BuscarEmpresasUseCase
{
  private readonly IEmpresaRepository _repository;

  public BuscarEmpresasUseCase(IEmpresaRepository repository)
  {
    _repository = repository;
  }

  public async Task<RespuestaPaginada<EmpresaDto>> ExecuteAsync(BuscarEmpresasQuery query)
  {
    (int total, List<Empresa> empresas) = await _repository.BuscarAsync(
      query.busqueda,
      query.estado,
      query.pagina,
      query.tamanioPagina
    );

    var items = empresas.Select(e => new EmpresaDto
    {
      Id = e.Id,
      Nombre = e.Nombre,
      Estado = e.Estado
    }).ToList();

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
