using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Repositories;

public class ModuloPerfilRepository : IModuloPerfilRepository
{
  private readonly AppDbContext _context;
  public ModuloPerfilRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<ModuloPerfil>> ObtenerPorPerfilAsync(int perfilId)
  {
    return await _context.ModulosPerfiles
        .Where(mp => mp.PerfilId == perfilId)
        .ToListAsync();
  }

  public async Task<ModuloPerfil?> ObtenerPorIdAsync(int id)
  {
    return await _context.ModulosPerfiles.FindAsync(id);
  }

  public async Task CrearAsync(ModuloPerfil moduloPerfil)
  {
    await _context.ModulosPerfiles.AddAsync(moduloPerfil);
    await _context.SaveChangesAsync();
  }

  public async Task EliminarAsync(int id)
  {
    var moduloPerfil = await ObtenerPorIdAsync(id);
    if (moduloPerfil != null)
    {
      _context.ModulosPerfiles.Remove(moduloPerfil);
      await _context.SaveChangesAsync();
    }
  }


}