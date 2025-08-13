namespace ApiPuertasAbiertas.Domain.Repositories;

using ApiPuertasAbiertas.Domain.Entities;
public interface IPerfilRepository
{
  Task<List<Perfil>> ObtenerTodosAsync();
}