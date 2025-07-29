using ApiPuertasAbiertas.Application.DTOs.Empresa;
using ApiPuertasAbiertas.Domain.Repositories;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.UseCases.Empresas;

public class EmpresaUseCases
{
  private readonly IEmpresaRepository _empresaRepository;
  private readonly IMapper _mapper;

  public EmpresaUseCases(IEmpresaRepository empresaRepository, IMapper mapper)
  {
    _empresaRepository = empresaRepository;
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
  public async Task CrearAsync(EmpresaDto empresaDto)
  {
    var empresa = _mapper.Map<Domain.Entities.Empresa>(empresaDto);
    await _empresaRepository.CrearAsync(empresa);
  }
  public async Task ActualizarAsync(EmpresaDto empresaDto)
  {
    var empresa = _mapper.Map<Domain.Entities.Empresa>(empresaDto);
    await _empresaRepository.ActualizarAsync(empresa);
  }
  public async Task EliminarAsync(int id)
  {
    await _empresaRepository.EliminarAsync(id);
  }
  public async Task<EmpresaDto?> BuscarPorNombreAsync(string nombre)
  {
    var empresa = await _empresaRepository.BuscarPorNombreAsync(nombre);
    return empresa == null ? null : _mapper.Map<EmpresaDto>(empresa);
  }
}