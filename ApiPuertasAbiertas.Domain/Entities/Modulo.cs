namespace ApiPuertasAbiertas.Domain.Entities;

public class Modulo
{
  public int Id { get; set; }
  public required string Nombre { get; set; }
  public required string Alias { get; set; }
  public ICollection<Perfil> Perfiles { get; set; } = new List<Perfil>();
  public ICollection<ModuloPerfil> ModulosPerfiles { get; set; } = new List<ModuloPerfil>();
}
