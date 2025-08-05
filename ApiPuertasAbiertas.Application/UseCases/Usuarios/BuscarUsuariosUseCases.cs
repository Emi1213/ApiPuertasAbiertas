using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Usuarios;

public class BuscarUsuariosUseCases
{
  private readonly IUsuarioRepository _repository;
  private readonly IMapper _mapper;

  public BuscarUsuariosUseCases(IUsuarioRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<RespuestaPaginada<UsuarioDto>> ExecuteAsync(BuscarUsuariosQuery query)
  {
    (int total, List<Usuario> usuarios) = await _repository.BuscarAsync(
      query.busqueda,
      query.perfilId,
      query.pagina,
      query.tamanioPagina
    );

    var items = _mapper.Map<List<UsuarioDto>>(usuarios);

    return new RespuestaPaginada<UsuarioDto>
    {
      Items = items,
      TotalItems = total,
      TotalPaginas = (int)Math.Ceiling((double)total / query.tamanioPagina),
      Pagina = query.pagina,
      TamanioPagina = query.tamanioPagina
    };
  }

}