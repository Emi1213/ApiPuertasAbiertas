using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.Profiles;

public class UsuarioProfile : Profile
{
  public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>().ReverseMap();
        CreateMap<Usuario, CrearUsuarioDto>().ReverseMap();
        CreateMap<Usuario, ActualizarUsuarioDto>().ReverseMap();
    }
}