using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiPuertasAbiertas.Application.DTOs.Auth;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace ApiPuertasAbiertas.Infrastructure.Services;

public class ServicioAuth : IServicioAuth
{
  private readonly IConfiguration _config;
  private readonly AppDbContext _context;
  public ServicioAuth(IConfiguration config, AppDbContext context)
  {
    _config = config;
    _context = context;
  }

  public async Task<AuthResponseDto?> AutenticarAsync(LoginDto dto)
  {
    var usuario = await _context.Usuarios
    .FirstOrDefaultAsync(u => u.NombreUsuario == dto.Usuario && u.Contrasenia == dto.Contrasenia);


    if (usuario == null)
      return null;

    var claims = new List<Claim>
    {
      new Claim(ClaimTypes.Name, usuario.NombreUsuario),
      new Claim(ClaimTypes.Role, usuario.Perfil?.Nombre ?? "Usuario")
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var expiracion = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"]));

    var token = new JwtSecurityToken(
      issuer: _config["JwtSettings:Issuer"],
      audience: _config["JwtSettings:Audience"],
      claims: claims,
      expires: expiracion,
      signingCredentials: creds
    );

    return new AuthResponseDto
    {
      Token = new JwtSecurityTokenHandler().WriteToken(token),
      Expiracion = expiracion,
    };

  }
}
