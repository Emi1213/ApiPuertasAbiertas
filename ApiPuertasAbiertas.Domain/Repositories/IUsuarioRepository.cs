using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;

public interface IUsuarioRepository
{
  Task<Usuario?> BuscarPorCredencialesAsync(string usuario, string contrasenia);
  Task<List<Usuario>> ObtenerTodosAsync();
  Task<Usuario?> ObtenerPorIdAsync(int id);
  Task CrearAsync(Usuario usuario);
  Task ActualizarAsync(Usuario usuario);
  Task EliminarAsync(int id);
}