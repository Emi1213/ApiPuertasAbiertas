namespace ApiPuertasAbiertas.Domain.Repositories;

using ApiPuertasAbiertas.Domain.Entities;
public interface IPerfilRepository
{
  Task<List<Perfil>> ObtenerTodosAsync();
  Task<Perfil?> ObtenerPorIdAsync(int id);
  Task CrearAsync(Perfil perfil);
  Task ActualizarAsync(Perfil perfil);
  Task EliminarAsync(int id);
  Task<(int total, List<Perfil>)> BuscarAsync(string? busqueda, int pagina, int tamanioPagina);
}