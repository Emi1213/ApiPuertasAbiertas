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
    return await _context.Usuarios
      .FirstOrDefaultAsync(u => u.NombreUsuario == usuario && u.Contrasenia == contrasenia);
  }

  public async Task<List<Usuario>> ObtenerTodosAsync()
  {
    return await _context.Usuarios.ToListAsync();
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
}