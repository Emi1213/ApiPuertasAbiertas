using ApiPuertasAbiertas.Domain.Entities;
using ApiPuertasAbiertas.Domain.Repositories;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
  private readonly AppDbContext _context;

  public UsuarioRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
  {
    return await _context.Usuarios
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
  }

  public async Task<List<Usuario>> ObtenerTodosAsync()
  {
    return await _context.Usuarios
            .Include(u => u.Perfil)
            .ToListAsync();
  }

  public async Task<Usuario?> ObtenerPorIdAsync(int id)
  {
    return await _context.Usuarios.FindAsync(id);
  }

  public async Task CrearAsync(Usuario usuario)
  {
    await _context.Usuarios.AddAsync(usuario);
    await _context.SaveChangesAsync();
  }

  public async Task ActualizarAsync(Usuario usuario)
  {
    _context.Usuarios.Update(usuario);
    await _context.SaveChangesAsync();
  }

  public async Task EliminarAsync(int id)
  {
    var usuario = await ObtenerPorIdAsync(id);
    if (usuario != null)
    {
      _context.Usuarios.Remove(usuario);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<(int total, List<Usuario>)> BuscarAsync(string? busqueda, int? perfilId, int pagina, int tamanioPagina)
  {
    var consulta = _context.Usuarios.Include(u => u.Perfil).AsQueryable();

    if (!string.IsNullOrWhiteSpace(busqueda))
    {
      consulta = consulta.Where(u => u.NombreUsuario.Contains(busqueda) || u.Nombre.Contains(busqueda));
    }
    if (perfilId.HasValue && perfilId.Value > 0)
    {
      consulta = consulta.Where(u => u.PerfilId == perfilId.Value);
    }

    var total = await consulta.CountAsync();
    var usuarios = await consulta
      .Skip((pagina - 1) * tamanioPagina)
      .Take(tamanioPagina)
      .ToListAsync();
    return (total, usuarios);
  }
}