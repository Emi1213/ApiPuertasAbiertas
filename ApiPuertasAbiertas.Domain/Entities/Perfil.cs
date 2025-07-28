namespace ApiPuertasAbiertas.Domain.Entities;

public class Perfil
{
  public int Id { get; set; }
  public string Nombre { get; set; } = null!;
  public string? Descripcion { get; set; }

  public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
