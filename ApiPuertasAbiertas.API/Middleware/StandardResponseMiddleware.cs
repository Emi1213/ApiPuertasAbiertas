using System.Text.Json;
using ApiPuertasAbiertas.Shared.Responses;
namespace ApiPuertasAbiertas.API.Middleware;

public class StandardResponseMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<StandardResponseMiddleware> _logger;
  public StandardResponseMiddleware(RequestDelegate next, ILogger<StandardResponseMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task Invoke(HttpContext context)
  {
    var originalBody = context.Response.Body;

    using var newBody = new MemoryStream();
    context.Response.Body = newBody;

    try
    {
      await _next(context);

      if (context.Response.ContentType?.Contains("application/json") == true && newBody.Length > 0)
      {
        newBody.Seek(0, SeekOrigin.Begin);
        var bodyText = await new StreamReader(newBody).ReadToEndAsync();

        object? originalData = null;
        try
        {
          originalData = JsonSerializer.Deserialize<object>(bodyText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch { }

        var wrapped = JsonSerializer.Serialize(new ApiRespuesta<object>
        {
          Exitoso = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
          Mensaje = context.Response.StatusCode switch
          {
            200 => "Operación exitosa",
            400 => "Solicitud incorrecta",
            401 => "No autorizado",
            500 => "Error interno",
            _ => "Resultado"
          },
          Datos = originalData
        });

        context.Response.Body = originalBody;
        context.Response.ContentLength = System.Text.Encoding.UTF8.GetByteCount(wrapped);
        await context.Response.WriteAsync(wrapped);
      }
      else
      {
        newBody.Seek(0, SeekOrigin.Begin);
        await newBody.CopyToAsync(originalBody);
      }
    }
    catch (Exception ex)
    {
      context.Response.Body = originalBody;
      context.Response.StatusCode = 500;
      context.Response.ContentType = "application/json";

      var error = JsonSerializer.Serialize(new ApiRespuesta<object>
      {
        Exitoso = false,
        Mensaje = ex.Message,
        Errores = new List<string> { ex.Message }
      });

      await context.Response.WriteAsync(error);
    }
  }
}