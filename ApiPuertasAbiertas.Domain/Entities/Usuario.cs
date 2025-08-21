namespace ApiPuertasAbiertas.Domain.Entities;

public class Usuario
{
  public int Id { get; set; }
  public required string NombreUsuario { get; set; }
  public required string Nombre { get; set; }
  public string? Descripcion { get; set; }
  public int? PerfilId { get; set; }  // Nullable porque puede no tener perfil asignado
  public Perfil? Perfil { get; set; }
  public ICollection<Ingreso> IngresosReconocidos { get; set; } = new List<Ingreso>();
}