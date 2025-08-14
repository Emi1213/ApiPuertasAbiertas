namespace ApiPuertasAbiertas.Domain.Entities
{
  public class Modulo
  {
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public string? Descripcion { get; set; }
  }
}