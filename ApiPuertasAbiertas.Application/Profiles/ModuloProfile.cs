using ApiPuertasAbiertas.Application.DTOs.Modulos;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;

public class ModuloProfile : Profile
{
  public ModuloProfile()
  {
    CreateMap<Modulo, ModuloDto>();
    CreateMap<CrearModuloDto, Modulo>();
  }
}
