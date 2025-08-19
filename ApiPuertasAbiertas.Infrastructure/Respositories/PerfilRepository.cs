using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Repositories;

public class PerfilRepository : IPerfilRepository
{
  private readonly AppDbContext _context;
  public PerfilRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<Perfil>> ObtenerTodosAsync()
  {
    return await _context.Perfiles
      .Include(p => p.ModulosPerfiles)
        .ThenInclude(mp => mp.Modulo)
      .ToListAsync();
  }

  public async Task<Perfil?> ObtenerPorIdAsync(int id)
  {
    return await _context.Perfiles
      .Include(p => p.ModulosPerfiles)
        .ThenInclude(mp => mp.Modulo)
      .FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task CrearAsync(Perfil perfil)
  {
    await _context.Perfiles.AddAsync(perfil);
    await _context.SaveChangesAsync();
  }

  public async Task ActualizarAsync(Perfil perfil)
  {
    _context.Perfiles.Update(perfil);
    await _context.SaveChangesAsync();
  }

  public async Task EliminarAsync(int id)
  {
    var perfil = await _context.Perfiles.FindAsync(id);
    if (perfil != null)
    {
      _context.Perfiles.Remove(perfil);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<(int total, List<Perfil>)> BuscarAsync(string? busqueda, int pagina, int tamanioPagina)
  {
    var query = _context.Perfiles.AsQueryable();

    if (!string.IsNullOrWhiteSpace(busqueda))
    {
      query = query.Where(p => p.Nombre.Contains(busqueda) ||
                              (p.Descripcion != null && p.Descripcion.Contains(busqueda)));
    }

    var total = await query.CountAsync();
    var perfiles = await query
      .Include(p => p.ModulosPerfiles)
        .ThenInclude(mp => mp.Modulo)
      .Skip((pagina - 1) * tamanioPagina)
      .Take(tamanioPagina)
      .ToListAsync();

    return (total, perfiles);
  }
}