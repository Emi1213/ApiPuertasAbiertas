using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ApiPuertasAbiertas.Infrastructure.Repositories;

public class PersonalRepository : IPersonalRepository
{
  private readonly AppDbContext _context;

  public PersonalRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<Personal>> ObtenerTodosAsync()
  {
    return await _context.Personal
            .Include(p => p.Empresa)
            .ToListAsync();
  }

  public async Task<Personal?> ObtenerPorIdAsync(int id)
  {
    return await _context.Personal
            .Include(p => p.Empresa)
            .FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task CrearAsync(Personal personal)
  {
    await _context.Personal.AddAsync(personal);
    await _context.SaveChangesAsync();
  }

  public async Task ActualizarAsync(Personal personal)
  {
    _context.Personal.Update(personal);
    await _context.SaveChangesAsync();
  }

  public async Task EliminarAsync(int id)
  {
    var personal = await ObtenerPorIdAsync(id);
    if (personal != null)
    {
      _context.Personal.Remove(personal);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<List<Personal>> ObtenerPorEmpresaIdAsync(int empresaId)
  {
    return await _context.Personal
            .Include(p => p.Empresa)
            .Where(p => p.EmpresaId == empresaId)
            .ToListAsync();
  }

  public async Task<(int total, List<Personal>)> BuscarAsync(string? busqueda, bool? estado, int pagina, int tamanioPagina)
  {
    var query = _context.Personal.AsQueryable();
    if (!string.IsNullOrWhiteSpace(busqueda))
    {
      query = query.Where(p => p.Nombres.Contains(busqueda) || p.Apellidos.Contains(busqueda));
    }

    if (estado.HasValue)
    {
      query = query.Where(p => p.Estado == estado.Value);
    }
    var total = await query.CountAsync();
    var personal = await query.Include(p => p.Empresa)
        .Skip((pagina - 1) * tamanioPagina)
        .Take(tamanioPagina)
        .ToListAsync();
    return (total, personal);
  }

  public async Task ActualizarEstadoPorEmpresaIdAsync(int empresaId, bool estado)
  {
    await _context.Personal
        .Where(p => p.EmpresaId == empresaId)
        .ExecuteUpdateAsync(p => p.SetProperty(x => x.Estado, estado));
  }
}