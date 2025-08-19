using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;

public interface IModuloRepository
{
  Task<(int total, List<Modulo>)> BuscarAsync(string? busqueda, int pagina, int tamanioPagina);
  Task<Modulo?> ObtenerPorIdAsync(int id);
  Task CrearAsync(Modulo modulo);
  Task EliminarAsync(int id);
}