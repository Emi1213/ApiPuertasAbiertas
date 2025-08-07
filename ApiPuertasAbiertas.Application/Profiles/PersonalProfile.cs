namespace ApiPuertasAbiertas.Application.Profiles;

using ApiPuertasAbiertas.Application.DTOs.Personal;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;
public class PersonalProfile : Profile
{
  public PersonalProfile()
  {
    CreateMap<Personal, PersonalDto>()
      .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa))
      .ReverseMap();

    CreateMap<CrearPersonalDto, Personal>()
      .ForMember(dest => dest.Empresa, opt => opt.Ignore())
      .ReverseMap();
    CreateMap<ActualizarPersonalDto, Personal>()
      .ForMember(dest => dest.Empresa, opt => opt.Ignore())
      .ReverseMap();

    CreateMap<PersonalDto, ActualizarPersonalDto>().ReverseMap();
    CreateMap<CrearPersonalDto, ActualizarPersonalDto>().ReverseMap();
    CreateMap<ActualizarPersonalDto, PersonalDto>().ReverseMap();
  }
}