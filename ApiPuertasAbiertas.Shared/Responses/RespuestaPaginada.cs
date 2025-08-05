namespace ApiPuertasAbiertas.Shared.Responses;

public class RespuestaPaginada<T>
{
  public List<T> Items { get; set; } = new();
  public int TotalItems { get; set; }
  public int TotalPaginas { get; set; }
  public int Pagina { get; set; }
  public int TamanioPagina { get; set; }
}
