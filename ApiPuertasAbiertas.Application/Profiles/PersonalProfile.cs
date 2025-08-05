namespace ApiPuertasAbiertas.Application.Profiles;

using ApiPuertasAbiertas.Application.DTOs.Personal;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;
public class PersonalProfile : Profile
{
  public PersonalProfile()
  {
    CreateMap<Personal, PersonalDto>().ReverseMap();
    CreateMap<PersonalDto, ActualizarPersonalDto>().ReverseMap();
    CreateMap<CrearPersonalDto, ActualizarPersonalDto>().ReverseMap();
    CreateMap<ActualizarPersonalDto, PersonalDto>().ReverseMap();
  }
}