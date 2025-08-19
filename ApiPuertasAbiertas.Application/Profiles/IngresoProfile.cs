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
      .ForMember(dest => dest.UsuarioRecon, opt => opt.MapFrom(src => src.UsuarioRecon))
      .ForMember(dest => dest.Alarmas, opt => opt.MapFrom(src => src.Alarmas));

    CreateMap<CrearIngresoDto, Ingreso>()
      .ForMember(dest => dest.Personal, opt => opt.Ignore())
      .ForMember(dest => dest.UsuarioRecon, opt => opt.Ignore())
      .ForMember(dest => dest.Alarmas, opt => opt.Ignore())
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.Estado, opt => opt.Ignore())
      .ForMember(dest => dest.UsuarioReconId, opt => opt.Ignore())
      .ForMember(dest => dest.FechaRecon, opt => opt.Ignore())
      .ForMember(dest => dest.Duracion, opt => opt.Ignore())
      .ForMember(dest => dest.TipoMotivo, opt => opt.Ignore())
      .ForMember(dest => dest.Causa, opt => opt.Ignore())
      .ReverseMap();

    CreateMap<ActualizarIngresoDto, Ingreso>()
      .ForMember(dest => dest.Personal, opt => opt.Ignore())
      .ForMember(dest => dest.UsuarioRecon, opt => opt.Ignore())
      .ForMember(dest => dest.Alarmas, opt => opt.Ignore())
      .ReverseMap();

    CreateMap<IngresoDto, ActualizarIngresoDto>().ReverseMap();
    CreateMap<CrearIngresoDto, ActualizarIngresoDto>().ReverseMap();
    CreateMap<Alarma, AlarmaDto>().ReverseMap();
  }
}