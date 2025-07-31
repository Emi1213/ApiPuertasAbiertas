namespace ApiPuertasAbiertas.Domain.Repositories;

using ApiPuertasAbiertas.Domain.Entities;
public interface IPersonalRepository
{
  Task<List<Personal>> ObtenerTodosAsync();
  Task<Personal?> ObtenerPorIdAsync(int id);
  Task CrearAsync(Personal personal);
  Task ActualizarAsync(Personal personal);
  Task EliminarAsync(int id);
  Task<List<Personal>> ObtenerPorEmpresaIdAsync(int empresaId);
}