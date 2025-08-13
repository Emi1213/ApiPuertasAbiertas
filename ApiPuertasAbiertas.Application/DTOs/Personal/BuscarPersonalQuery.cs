namespace ApiPuertasAbiertas.Application.DTOs.Personal;

public class BuscarPersonalQuery
{
  public string? busqueda { get; set; }
  public bool? estado { get; set; }
  public int? empresaId { get; set; } // Nuevo filtro por id de empresa
  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}