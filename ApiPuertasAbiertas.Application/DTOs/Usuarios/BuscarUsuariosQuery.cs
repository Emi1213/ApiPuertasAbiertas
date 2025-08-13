namespace ApiPuertasAbiertas.Application.DTOs.Empresa;

public class BuscarUsuariosQuery
{
  public string? busqueda { get; set; }
  public int? perfilId { get; set; }
  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}
