using ApiPuertasAbiertas.Application.DTOs.Perfil;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.Profiles;
public class PerfilProfile : Profile
{
    public PerfilProfile()
    {
        CreateMap<Perfil, PerfilDto>().ReverseMap();
        // CreateMap<Perfil, CrearPerfilDto>().ReverseMap();
        // CreateMap<Perfil, ActualizarPerfilDto>().ReverseMap();
    }
}