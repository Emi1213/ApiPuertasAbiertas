using AutoMapper;
using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Application.DTOs.Empresa;

public class EmpresaProfile : Profile
{
  public EmpresaProfile()
  {
    CreateMap<Empresa, EmpresaDto>().ReverseMap();
    CreateMap<CrearEmpresaDto, Empresa>();
  }
}
