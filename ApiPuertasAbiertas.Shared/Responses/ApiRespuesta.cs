namespace ApiPuertasAbiertas.Shared.Responses;

public class ApiRespuesta<T>
{
  public bool Exitoso { get; set; }
  public string? Mensaje { get; set; }
  public T? Datos { get; set; }
}
