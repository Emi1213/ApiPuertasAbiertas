using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiPuertasAbiertas.Application.DTOs.Auth;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Infrastructure.Services;

public class ServicioAuth : IServicioAuth
{
  private readonly IConfiguration _config;
  public ServicioAuth(IConfiguration config)
  {
    _config = config;

  }

  public string GenerarToken(Usuario usuario)
  {
    var claims = new[]
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

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
