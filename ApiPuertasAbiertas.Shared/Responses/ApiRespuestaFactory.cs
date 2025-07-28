namespace ApiPuertasAbiertas.Shared.Responses;

public static class ApiRespuestaFactory
{
  public static ApiRespuesta<T> CrearExito<T>(T datos, string mensaje)
  {
    return new ApiRespuesta<T>
    {
      exitoso = true,
      mensaje = mensaje,
      datos = datos
    };
  }

  public static ApiRespuesta<T> CrearError<T>(List<string> errores, string mensaje)
  {
    return new ApiRespuesta<T>
    {
      exitoso = false,
      mensaje = mensaje,
      errores = errores
    };
  }
}
