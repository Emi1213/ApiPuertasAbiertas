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
}