namespace ApiPuertasAbiertas.Application.DTOs.Empresa;

public class BuscarEmpresasQuery
{
  public string? busqueda { get; set; }
  public bool? estado { get; set; }
  public int pagina { get; set; } = 1;
  public int tamanioPagina { get; set; } = 10;
}
