namespace ApiPuertasAbiertas.Domain.Entities;

public class Ingreso
{
  public int Id { get; set; }
  public required DateTime FechaInicio { get; set; }
  public DateTime? FechaFin { get; set; }
  public string? Duracion { get; set; }
  public string? Comentario { get; set; }
  public string? IdMotivo { get; set; }
  public string? TipoMotivo { get; set; }
  public string? Causa { get; set; }
  public DateTime? FechaRecon { get; set; }
  public int? UsuarioReconId { get; set; }
  public Usuario? UsuarioRecon { get; set; }
  public string Estado { get; set; } = "En proceso";
  public int PersonalId { get; set; }
  public Personal? Personal { get; set; }
}