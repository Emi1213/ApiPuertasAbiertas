using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Domain.Entities;
using AutoMapper;

namespace ApiPuertasAbiertas.Application.Profiles;

public class UsuarioProfile : Profile
{
  public UsuarioProfile()
  {
    CreateMap<Usuario, UsuarioDto>()
        .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.NombreUsuario))
        .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion ?? string.Empty))
        .ReverseMap();
    CreateMap<CrearUsuarioDto, Usuario>()
        .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario));

    CreateMap<Usuario, CrearUsuarioDto>()
        .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.NombreUsuario));

    // Para actualización
    CreateMap<ActualizarUsuarioDto, Usuario>()
        .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario));

    CreateMap<Usuario, ActualizarUsuarioDto>()
        .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.NombreUsuario));
  }
}