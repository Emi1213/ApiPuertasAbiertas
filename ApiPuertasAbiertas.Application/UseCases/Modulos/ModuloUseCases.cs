using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Modulos;

public class ModuloUseCases
{
  private readonly IModuloRepository _moduloRepository;
  private readonly IMapper _mapper;

  public ModuloUseCases(IModuloRepository moduloRepository, IMapper mapper)
  {
    _moduloRepository = moduloRepository;
    _mapper = mapper;
  }

  public async Task<List<ModuloDto>> ObtenerTodosAsync()
  {
    var modulos = await _moduloRepository.ObtenerTodosAsync();
    return _mapper.Map<List<ModuloDto>>(modulos);
  }

  public async Task<ModuloDto?> ObtenerPorIdAsync(int id)
  {
    var modulo = await _moduloRepository.ObtenerPorIdAsync(id);
    return _mapper.Map<ModuloDto?>(modulo);
  }

  public async Task<ModuloDto> CrearAsync(CrearModuloDto moduloDto)
  {
    var modulo = _mapper.Map<Modulo>(moduloDto);
    await _moduloRepository.CrearAsync(modulo);
    return _mapper.Map<ModuloDto>(modulo);
  }

  public async Task EliminarAsync(int id)
  {
    var modulo = await _moduloRepository.ObtenerPorIdAsync(id);
    if (modulo == null) throw new KeyNotFoundException("Módulo no encontrado");
    await _moduloRepository.EliminarAsync(id);
  }

}
