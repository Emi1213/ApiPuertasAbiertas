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

  public async Task<List<Modulo>> ObtenerPorPerfilAsync(int perfilId)
  {
    return await _context.ModulosPerfiles
        .Where(mp => mp.PerfilId == perfilId)
        .Select(mp => mp.Modulo)
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

  public async Task EliminarPorPerfilAsync(int perfilId)
  {
    var modulosPerfiles = await _context.ModulosPerfiles
        .Where(mp => mp.PerfilId == perfilId)
        .ToListAsync();

    _context.ModulosPerfiles.RemoveRange(modulosPerfiles);
    await _context.SaveChangesAsync();
  }

  public async Task AsignarModulosAsync(int perfilId, List<int> modulosIds)
  {
    var modulosPerfiles = modulosIds.Select(moduloId => new ModuloPerfil
    {
      PerfilId = perfilId,
      ModuloId = moduloId,
      // Necesitamos cargar las entidades relacionadas, pero EF las manejará
      Modulo = null!,
      Perfil = null!
    }).ToList();

    await _context.ModulosPerfiles.AddRangeAsync(modulosPerfiles);
    await _context.SaveChangesAsync();
  }


}