using ApiPuertasAbiertas.Application.Common;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Application.Profiles;
using ApiPuertasAbiertas.Application.UseCases.Auth;
using ApiPuertasAbiertas.Application.UseCases.Empresas;
using ApiPuertasAbiertas.Application.UseCases.Ingresos;
using ApiPuertasAbiertas.Application.UseCases.Modulos;
using ApiPuertasAbiertas.Application.UseCases.ModulosPerfil;
using ApiPuertasAbiertas.Application.UseCases.Perfiles;
using ApiPuertasAbiertas.Application.UseCases.Personal;
using ApiPuertasAbiertas.Application.UseCases.Usuarios;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Repositories;
using ApiPuertasAbiertas.Infrastructure.Services;

namespace ApiPuertasAbiertas.API.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<ActiveDirectorySettings>(
        configuration.GetSection("ActiveDirectorySettings"));

    return services;
  }

  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<LoginUseCase>();
    services.AddScoped<UsuarioUseCases>();
    services.AddScoped<BuscarUsuariosUseCases>();
    services.AddScoped<IBuscarUsuariosActiveDirectoryUseCase, BuscarUsuariosActiveDirectoryUseCase>();
    services.AddScoped<EmpresaUseCases>();
    services.AddScoped<BuscarEmpresasUseCase>();
    services.AddScoped<PersonalUseCases>();
    services.AddScoped<BuscarPersonalUseCases>();
    services.AddScoped<IngresoUseCases>();
    services.AddScoped<BuscarIngresosUseCases>();
    services.AddScoped<ReconocerIngresoUseCase>();
    services.AddScoped<ModuloUseCases>();
    services.AddScoped<BuscarModulosUseCases>();
    services.AddScoped<ModulosNavegacionUseCases>();
    services.AddScoped<ModulosPerfilUseCases>();
    services.AddScoped<PerfilUseCases>();
    services.AddScoped<BuscarPerfilesUseCases>();

    return services;
  }

  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    services.AddScoped<IServicioAuth, ServicioAuth>();
    services.AddScoped<IActiveDirectoryServices, ActiveDirectoryServices>();
    services.AddSingleton<IClock, EcuadorClock>();
    services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    services.AddScoped<IEmpresaRepository, EmpresaRepository>();
    services.AddScoped<IPersonalRepository, PersonalRepository>();
    services.AddScoped<IIngresoRepository, IngresoRepository>();
    services.AddScoped<IModuloRepository, ModuloRepository>();
    services.AddScoped<IModuloPerfilRepository, ModuloPerfilRepository>();
    services.AddScoped<IPerfilRepository, PerfilRepository>();

    return services;
  }

  public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
  {
    services.AddAutoMapper(typeof(UsuarioProfile));
    services.AddAutoMapper(typeof(PerfilProfile));
    services.AddAutoMapper(typeof(EmpresaProfile));
    services.AddAutoMapper(typeof(PersonalProfile));
    services.AddAutoMapper(typeof(IngresoProfile));
    services.AddAutoMapper(typeof(ModuloProfile));

    return services;
  }
}
