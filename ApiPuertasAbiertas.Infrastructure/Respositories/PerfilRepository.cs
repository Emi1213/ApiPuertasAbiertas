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
    return await _context.Perfiles.ToListAsync();
  }

  public async Task<Perfil?> ObtenerPorIdAsync(int id)
  {
    return await _context.Perfiles.FindAsync(id);
  }
}