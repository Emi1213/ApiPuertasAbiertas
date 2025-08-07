namespace ApiPuertasAbiertas.Application.DTOs.Personal;

public class BuscarPersonalQuery
{
  public string? busqueda { get; set; }
  public bool? estado { get; set; }
  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}