using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Usuarios;

public class UsuarioUseCases
{
  private readonly IUsuarioRepository _usuarioRepository;
  private readonly IMapper _mapper;

  public UsuarioUseCases(IUsuarioRepository usuarioRepository, IMapper mapper)
  {
    _mapper = mapper;
    _usuarioRepository = usuarioRepository;
  }

  public async Task<List<UsuarioDto>> ObtenerTodosAsync()
  {
    var usuarios = await _usuarioRepository.ObtenerTodosAsync();
    return _mapper.Map<List<UsuarioDto>>(usuarios);
  }

  public async Task<UsuarioDto?> ObtenerPorIdAsync(int id)
  {
    var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
    return usuario == null ? null : _mapper.Map<UsuarioDto>(usuario);
  }

  public async Task CrearAsync(CrearUsuarioDto dto)
  {
    var usuario = _mapper.Map<Domain.Entities.Usuario>(dto);
    await _usuarioRepository.CrearAsync(usuario);
  }

  public async Task ActualizarAsync(ActualizarUsuarioDto dto)
  {
    var usuario = await _usuarioRepository.ObtenerPorIdAsync(dto.Id);
    if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado");
    // Map only the fields that can be updated

    _mapper.Map(dto, usuario);
    await _usuarioRepository.ActualizarAsync(usuario);
  }

  public async Task EliminarAsync(int id)
  {
    await _usuarioRepository.EliminarAsync(id);
  }
}