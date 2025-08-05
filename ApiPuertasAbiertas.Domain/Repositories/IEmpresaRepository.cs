using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;

public interface IEmpresaRepository
{
  Task<Empresa?> BuscarPorNombreAsync(string nombre);
  Task<List<Empresa>> ObtenerTodasAsync();
  Task<Empresa?> ObtenerPorIdAsync(int id);
  Task CrearAsync(Empresa empresa);
  Task ActualizarAsync(Empresa empresa);
  Task EliminarAsync(int id);
  Task<(int total, List<Empresa>)> BuscarAsync(string? nombre, bool? estado, int pagina, int tamanioPagina);

}