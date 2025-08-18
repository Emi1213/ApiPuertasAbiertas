using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Repositories;

public class ModuloRepository : IModuloRepository
{
  private readonly AppDbContext _context;
  public ModuloRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<(int total, List<Modulo>)> BuscarAsync(string? busqueda, int pagina, int tamanioPagina)
  {
    var query = _context.Modulos.AsQueryable();
    if (!string.IsNullOrWhiteSpace(busqueda))
      query = query.Where(m => m.Nombre.Contains(busqueda) || m.Alias.Contains(busqueda));

    var total = await query.CountAsync();

    var modulos = await query
        .Skip((pagina - 1) * tamanioPagina)
        .Take(tamanioPagina)
        .ToListAsync();

    return (total, modulos);
  }


  public async Task<Modulo?> ObtenerPorIdAsync(int id)
  {
    return await _context.Modulos.FindAsync(id);
  }

  public async Task CrearAsync(Modulo modulo)
  {
    await _context.Modulos.AddAsync(modulo);
    await _context.SaveChangesAsync();
  }

  public async Task EliminarAsync(int id)
  {
    var modulo = await ObtenerPorIdAsync(id);
    if (modulo != null)
    {
      _context.Modulos.Remove(modulo);
      await _context.SaveChangesAsync();
    }
  }

  public Task<List<Modulo>> ObtenerTodosAsync()
  {
    throw new NotImplementedException();
  }

}