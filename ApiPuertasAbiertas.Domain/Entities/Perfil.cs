namespace ApiPuertasAbiertas.Domain.Entities;

public class Perfil
{
  public int Id { get; set; }
  public string Nombre { get; set; } = null!;
  public string? Descripcion { get; set; }
  public int RbacVersion { get; set; } = 1;
  public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
  public ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();
  public ICollection<ModuloPerfil> ModulosPerfiles { get; set; } = new List<ModuloPerfil>();
}
