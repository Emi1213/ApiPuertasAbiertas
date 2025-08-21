using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Shared.Responses;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Usuarios;

public class BuscarUsuariosUseCases
{
  private readonly IUsuarioRepository _usuarioRepository;
  private readonly IMapper _mapper;

  public BuscarUsuariosUseCases(IUsuarioRepository usuarioRepository, IMapper mapper)
  {
    _usuarioRepository = usuarioRepository;
    _mapper = mapper;
  }

  public async Task<RespuestaPaginada<UsuarioDto>> ExecuteAsync(BuscarUsuariosQuery consulta)
  {
    var (total, usuarios) = await _usuarioRepository.BuscarAsync(
      consulta.busqueda,
      consulta.perfilId,
      consulta.pagina,
      consulta.tamanioPagina
    );

    var elementos = _mapper.Map<List<UsuarioDto>>(usuarios);

    return new RespuestaPaginada<UsuarioDto>
    {
      Items = elementos,
      TotalItems = total,
      TotalPaginas = (int)Math.Ceiling((double)total / consulta.tamanioPagina),
      Pagina = consulta.pagina,
      TamanioPagina = consulta.tamanioPagina
    };
  }
}