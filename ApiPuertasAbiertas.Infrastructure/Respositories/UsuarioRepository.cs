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
  public async Task<Usuario?> BuscarPorCredencialesAsync(string usuario, string contrasenia)
  {
    return await _context.Usuarios.Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.NombreUsuario == usuario && u.Contrasenia == contrasenia);
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
    var query = _context.Usuarios.Include(u => u.Perfil).AsQueryable();

    if (!string.IsNullOrWhiteSpace(busqueda))
    {
      query = query.Where(u => u.NombreUsuario.Contains(busqueda) || u.Nombre.Contains(busqueda));
    }

    // Solo filtrar por perfilId si se especifica explícitamente y es mayor que 0
    // Si no se especifica o es 0/null, devolver todos los usuarios (incluidos los sin perfil)
    if (perfilId.HasValue && perfilId.Value > 0)
    {
      query = query.Where(u => u.PerfilId == perfilId.Value);
    }

    var total = await query.CountAsync();
    var usuarios = await query
      .Skip((pagina - 1) * tamanioPagina)
      .Take(tamanioPagina)
      .ToListAsync();
    return (total, usuarios);
  }
}