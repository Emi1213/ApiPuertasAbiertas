using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Usuarios;

public class UsuarioUseCases
{
  private readonly IUsuarioRepository _usuarioRepository;
  private readonly IMapper _mapper;

  public UsuarioUseCases(IUsuarioRepository usuarioRepository)
  {
    _usuarioRepository = usuarioRepository;
  }


}