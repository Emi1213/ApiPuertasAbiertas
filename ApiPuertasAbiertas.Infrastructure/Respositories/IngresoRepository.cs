using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Repositories;

public class IngresoRepository : IIngresoRepository
{
  private readonly AppDbContext _context;

  public IngresoRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<Ingreso>> ObtenerTodosAsync()
  {
    return await _context.Ingresos
      .Include(i => i.Personal)
        .ThenInclude(p => p!.Empresa)
      .ToListAsync();
  }

  public async Task<Ingreso?> ObtenerPorIdAsync(int id)
  {
    return await _context.Ingresos
      .Include(i => i.Personal)
        .ThenInclude(p => p!.Empresa)
      .FirstOrDefaultAsync(i => i.Id == id);
  }

  public async Task CrearAsync(Ingreso ingreso)
  {
    await _context.Ingresos.AddAsync(ingreso);
    await _context.SaveChangesAsync();
  }

  public async Task ActualizarAsync(Ingreso ingreso)
  {
    _context.Ingresos.Update(ingreso);
    await _context.SaveChangesAsync();
  }

  public async Task EliminarAsync(int id)
  {
    var ingreso = await _context.Ingresos.FindAsync(id);
    if (ingreso != null)
    {
      _context.Ingresos.Remove(ingreso);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<List<Ingreso>> ObtenerPorPersonalIdAsync(int personalId)
  {
    return await _context.Ingresos
      .Include(i => i.Personal)
        .ThenInclude(p => p!.Empresa)
      .Where(i => i.PersonalId == personalId)
      .ToListAsync();
  }

  public async Task<(int total, List<Ingreso>)> BuscarAsync(string? busqueda, int pagina, int tamanioPagina)
  {
    var query = _context.Ingresos.AsQueryable();

    if (!string.IsNullOrWhiteSpace(busqueda))
    {
      query = query.Where(i => (i.Causa != null && i.Causa.Contains(busqueda)) ||
                              (i.Comentario != null && i.Comentario.Contains(busqueda)) ||
                              (i.Personal != null && i.Personal.Nombres.Contains(busqueda)) ||
                              (i.Personal != null && i.Personal.Apellidos.Contains(busqueda)));
    }

    var total = await query.CountAsync();
    var ingresos = await query
      .Include(i => i.Personal)
        .ThenInclude(p => p!.Empresa)
      .Skip((pagina - 1) * tamanioPagina)
      .Take(tamanioPagina)
      .ToListAsync();

    return (total, ingresos);
  }
}