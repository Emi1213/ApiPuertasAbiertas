namespace ApiPuertasAbiertas.Shared.Responses;

public static class ApiRespuestaFactory
{
  public static ApiRespuesta<T> CrearExito<T>(T datos, string mensaje)
  {
    return new ApiRespuesta<T>
    {
      Exitoso = true,
      Mensaje = mensaje,
      Datos = datos
    };
  }

  public static ApiRespuesta<T> CrearError<T>(string mensaje)
  {
    return new ApiRespuesta<T>
    {
      Exitoso = false,
      Mensaje = mensaje,
      Datos = default
    };
  }
}
