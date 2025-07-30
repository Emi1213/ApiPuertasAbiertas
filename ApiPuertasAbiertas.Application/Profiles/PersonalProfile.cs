namespace ApiPuertasAbiertas.Application.Profiles;

using ApiPuertasAbiertas.Application.DTOs.Personal;
using AutoMapper;
public class PersonalProfile : Profile
{
  public PersonalProfile()
  {
    CreateMap<PersonalDto, ActualizarPersonalDto>().ReverseMap();
    CreateMap<CrearPersonalDto, ActualizarPersonalDto>().ReverseMap();
    CreateMap<ActualizarPersonalDto, PersonalDto>().ReverseMap();
  }
}