namespace ApiPuertasAbiertas.Application.DTOs.Modulos;

public class BuscarModulosQuery
{
  public string? busqueda { get; set; }
  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}
