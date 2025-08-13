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
      .Include(i => i.UsuarioRecon)
      .ToListAsync();
  }

  public async Task<Ingreso?> ObtenerPorIdAsync(int id)
  {
    return await _context.Ingresos
      .Include(i => i.Personal)
        .ThenInclude(p => p!.Empresa)
      .Include(i => i.UsuarioRecon)
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
      .Include(i => i.UsuarioRecon)
      .Where(i => i.PersonalId == personalId)
      .ToListAsync();
  }

  public async Task<(int total, List<Ingreso>)> BuscarAsync(string? busqueda, string? estado, int pagina, int tamanioPagina)
  {
    var query = _context.Ingresos.AsQueryable();

    if (!string.IsNullOrWhiteSpace(busqueda))
    {
      query = query.Where(i => (i.Causa != null && i.Causa.Contains(busqueda)) ||
                              (i.Comentario != null && i.Comentario.Contains(busqueda)) ||
                              (i.Personal != null && i.Personal.Nombres.Contains(busqueda)) ||
                              (i.Personal != null && i.Personal.Apellidos.Contains(busqueda)));
    }

    if (!string.IsNullOrWhiteSpace(estado))
    {
      query = query.Where(i => i.Estado == estado);
    }

    var total = await query.CountAsync();
    var ingresos = await query
      .Include(i => i.Personal)
        .ThenInclude(p => p!.Empresa)
      .Include(i => i.UsuarioRecon)
      .Skip((pagina - 1) * tamanioPagina)
      .Take(tamanioPagina)
      .ToListAsync();

    return (total, ingresos);
  }

  public async Task<bool> ReconocerAsync(int id, int usuarioId, DateTime fechaUtc)
  {
    var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
    if (!usuarioExiste)
      return false;
    var filas = await _context.Ingresos
      .Where(i => i.Id == id && i.FechaRecon == null)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.FechaRecon, fechaUtc)
        .SetProperty(i => i.UsuarioReconId, usuarioId)
      );

    return filas > 0;
  }

  public async Task<bool> QuitarReconocimientoAsync(int id)
  {
    var filas = await _context.Ingresos
      .Where(i => i.Id == id && i.FechaRecon != null)
      .ExecuteUpdateAsync(setters => setters
        .SetProperty(i => i.FechaRecon, (DateTime?)null)
        .SetProperty(i => i.UsuarioReconId, (int?)null)
      );

    return filas > 0;
  }
}