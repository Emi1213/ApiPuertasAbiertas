using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;

public interface IIngresoRepository
{
  Task<List<Ingreso>> ObtenerTodosAsync();
  Task<Ingreso?> ObtenerPorIdAsync(int id);
  Task CrearAsync(Ingreso ingreso);
  Task ActualizarAsync(Ingreso ingreso);
  Task EliminarAsync(int id);
  Task<List<Ingreso>> ObtenerPorPersonalIdAsync(int personalId);
  Task<(int total, List<Ingreso>)> BuscarAsync(string? busqueda, string? estado, int pagina, int tamanioPagina);
  Task<bool> ReconocerAsync(int id, int usuario, DateTime fechaUtc);
  Task<bool> QuitarReconocimientoAsync(int id);
}