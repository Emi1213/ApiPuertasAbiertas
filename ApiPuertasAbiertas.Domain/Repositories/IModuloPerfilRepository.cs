using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;

public interface IModuloPerfilRepository
{
  Task<List<ModuloPerfil>> ObtenerPorPerfilAsync(int perfilId);
  Task<ModuloPerfil?> ObtenerPorIdAsync(int id);
  Task CrearAsync(ModuloPerfil moduloPerfil);
  Task EliminarAsync(int id);
}
