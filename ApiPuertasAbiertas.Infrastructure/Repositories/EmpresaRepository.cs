using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
  private readonly AppDbContext _context;

  public EmpresaRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Empresa?> BuscarPorNombreAsync(string nombre)
  {
    return await _context.Empresas
    .FirstOrDefaultAsync(e => e.Nombre == nombre);
  }

  public async Task<List<Empresa>> ObtenerTodasAsync()
  {
    return await _context.Empresas.ToListAsync();
  }

  public async Task<Empresa?> ObtenerPorIdAsync(int id)
  {
    return await _context.Empresas.FindAsync(id);
  }

  public async Task CrearAsync(Empresa empresa)
  {
    await _context.Empresas.AddAsync(empresa);
    await _context.SaveChangesAsync();
  }

  public async Task ActualizarAsync(Empresa empresa)
  {
    _context.Empresas.Update(empresa);
    await _context.SaveChangesAsync();
  }

  public async Task<(int total, List<Empresa>)> BuscarAsync(string? nombre, bool? estado, int pagina, int tamanioPagina)
{
  var query = _context.Empresas.AsQueryable();

  if (!string.IsNullOrWhiteSpace(nombre))
    query = query.Where(e => e.Nombre.Contains(nombre));

  if (estado.HasValue)
    query = query.Where(e => e.Estado == estado.Value);

  var total = await query.CountAsync();

  var empresas = await query
    .Skip((pagina - 1) * tamanioPagina)
    .Take(tamanioPagina)
    .ToListAsync();

  return (total, empresas);
}


  public async Task EliminarAsync(int id)
  {
    var empresa = await ObtenerPorIdAsync(id);
    if (empresa != null)
    {
      _context.Empresas.Remove(empresa);
      await _context.SaveChangesAsync();
    }
  }
}