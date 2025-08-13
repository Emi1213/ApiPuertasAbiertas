namespace ApiPuertasAbiertas.Application.DTOs.Ingresos;

public class BuscarIngresosQuery
{
  public string? busqueda { get; set; }
  public string? estado { get; set; }
  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}