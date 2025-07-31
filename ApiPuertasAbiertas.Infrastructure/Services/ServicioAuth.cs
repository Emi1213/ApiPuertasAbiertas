using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiPuertasAbiertas.Application.Interfaces;
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
      new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
      new Claim(ClaimTypes.Name, usuario.NombreUsuario),
      new Claim(ClaimTypes.Role, usuario.Perfil.Nombre.ToString()),
    };
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


    var token = new JwtSecurityToken(
      issuer: _config["JwtSettings:Issuer"],
      audience: _config["JwtSettings:Audience"],
      claims: claims,
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
