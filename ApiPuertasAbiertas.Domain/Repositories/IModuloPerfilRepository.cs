using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;

public interface IModuloPerfilRepository
{
  Task<List<Modulo>> ObtenerPorPerfilAsync(int perfilId);
  Task<ModuloPerfil?> ObtenerPorIdAsync(int id);
  Task CrearAsync(ModuloPerfil moduloPerfil);
  Task EliminarAsync(int id);
  Task EliminarPorPerfilAsync(int perfilId);
  Task AsignarModulosAsync(int perfilId, List<int> modulosIds);
}
