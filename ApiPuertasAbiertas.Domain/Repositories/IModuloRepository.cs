using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;

public interface IModuloRepository
{
  Task<(int total, List<Modulo>)> BuscarAsync(string? busqueda, int pagina, int tamanioPagina);

  Task<List<Modulo>> ObtenerTodosAsync();
  Task<Modulo?> ObtenerPorPerfilAsync(int perfilId);
  Task CrearAsync(Modulo modulo);
  Task EliminarAsync(int id);
}