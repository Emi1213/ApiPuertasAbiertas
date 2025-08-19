using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.Profiles;

public class PerfilProfile : Profile
{
  public PerfilProfile()
  {
    CreateMap<Perfil, PerfilDto>()
        .ForMember(dest => dest.Modulos, opt => opt.MapFrom(src => src.ModulosPerfiles.Select(mp => mp.Modulo)))
        .ReverseMap();

    CreateMap<CrearPerfilDto, Perfil>()
        .ForMember(dest => dest.ModulosPerfiles, opt => opt.Ignore())
        .ReverseMap();

    CreateMap<ActualizarPerfilDto, Perfil>()
        .ForMember(dest => dest.ModulosPerfiles, opt => opt.Ignore())
        .ReverseMap();
  }
}