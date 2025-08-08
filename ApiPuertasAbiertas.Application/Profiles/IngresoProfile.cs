using ApiPuertasAbiertas.Application.DTOs.Ingresos;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.Profiles;

public class IngresoProfile : Profile
{
  public IngresoProfile()
  {
    CreateMap<Ingreso, IngresoDto>()
      .ForMember(dest => dest.Personal, opt => opt.MapFrom(src => src.Personal))
      .ReverseMap();

    CreateMap<CrearIngresoDto, Ingreso>()
      .ForMember(dest => dest.Personal, opt => opt.Ignore())
      .ReverseMap();

    CreateMap<ActualizarIngresoDto, Ingreso>()
      .ForMember(dest => dest.Personal, opt => opt.Ignore())
      .ReverseMap();

    CreateMap<IngresoDto, ActualizarIngresoDto>().ReverseMap();
    CreateMap<CrearIngresoDto, ActualizarIngresoDto>().ReverseMap();
  }
}