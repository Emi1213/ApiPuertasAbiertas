using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Empresas;

public class EmpresaUseCases
{
  private readonly IEmpresaRepository _empresaRepository;
  private readonly IPersonalRepository _personalRepository;
  private readonly IMapper _mapper;

  public EmpresaUseCases(IEmpresaRepository empresaRepository, IPersonalRepository personalRepository, IMapper mapper)
  {
    _empresaRepository = empresaRepository;
    _personalRepository = personalRepository;
    _mapper = mapper;
  }
  public async Task<List<EmpresaDto>> ObtenerTodosAsync()
  {
    var empresas = await _empresaRepository.ObtenerTodasAsync();
    return _mapper.Map<List<EmpresaDto>>(empresas);
  }
  public async Task<EmpresaDto?> ObtenerPorIdAsync(int id)
  {
    var empresa = await _empresaRepository.ObtenerPorIdAsync(id);
    return empresa == null ? null : _mapper.Map<EmpresaDto>(empresa);
  }
  public async Task<EmpresaDto> CrearAsync(CrearEmpresaDto empresaDto)
  {
    var empresa = _mapper.Map<Domain.Entities.Empresa>(empresaDto);
    await _empresaRepository.CrearAsync(empresa);
    return _mapper.Map<EmpresaDto>(empresa);
  }
  public async Task ActualizarAsync(EmpresaDto empresaDto)
  {
    var empresaExistente = await _empresaRepository.ObtenerPorIdAsync(empresaDto.Id);
    if (empresaExistente == null)
    {
      throw new KeyNotFoundException("Empresa no encontrada");
    }

    bool empresaSeDesactiva = empresaExistente.Estado && !empresaDto.Estado;

    _mapper.Map(empresaDto, empresaExistente);
    await _empresaRepository.ActualizarAsync(empresaExistente);
    if (empresaSeDesactiva)
    {
      await _personalRepository.ActualizarEstadoPorEmpresaIdAsync(empresaDto.Id, false);
    }
  }
  public async Task EliminarAsync(int id)
  {
    var empresa = await _empresaRepository.ObtenerPorIdAsync(id);
    if (empresa == null) throw new KeyNotFoundException("Empresa no encontrada");

    await _empresaRepository.EliminarAsync(id);
  }
  public async Task<EmpresaDto?> BuscarPorNombreAsync(string nombre)
  {
    var empresa = await _empresaRepository.BuscarPorNombreAsync(nombre);
    return empresa == null ? null : _mapper.Map<EmpresaDto>(empresa);
  }
}