namespace ApiPuertasAbiertas.Application.DTOs.Perfiles;

public class BuscarPerfilesQuery
{
  public string? busqueda { get; set; }
  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}
