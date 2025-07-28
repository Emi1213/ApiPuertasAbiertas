namespace ApiPuertasAbiertas.Shared.Responses;

public class ApiRespuesta<T>
{
  public bool exitoso { get; set; }
  public string? mensaje { get; set; }
  public List<string> errores { get; set; } = new List<string>();
  public T? datos { get; set; }
}