namespace ApiPuertasAbiertas.Shared.Responses;

public class ApiRespuesta<T>
{
  public bool Exitoso { get; set; }
  public string? Mensaje { get; set; }
  public List<string> Errores { get; set; } = new List<string>();
  public T? Datos { get; set; }
}