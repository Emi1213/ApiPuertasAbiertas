using System.Text;
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
                List<string>? errores = null;
                string mensaje = "Resultado";
                bool esErrorValidacion = false;

                try
                {
                    using var doc = JsonDocument.Parse(bodyText);

                    // Detectar ValidationProblemDetails
                    if (doc.RootElement.TryGetProperty("title", out var titleProp) &&
                        titleProp.GetString()?.Contains("validation", StringComparison.OrdinalIgnoreCase) == true &&
                        doc.RootElement.TryGetProperty("errors", out var errorsProp))
                    {
                        errores = new List<string>();

                        foreach (var prop in errorsProp.EnumerateObject())
                        {
                            foreach (var err in prop.Value.EnumerateArray())
                            {
                                errores.Add($"{(string.IsNullOrWhiteSpace(prop.Name) || prop.Name == "$" ? "Entrada" : prop.Name)}: {err.GetString()}");
                            }
                        }

                        mensaje = "Error de validación";
                        esErrorValidacion = true;
                    }
                    else
                    {
                        originalData = JsonSerializer.Deserialize<object>(bodyText, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        mensaje = context.Response.StatusCode switch
                        {
                            200 => "Operación exitosa",
                            400 => "Solicitud incorrecta",
                            401 => "No autorizado",
                            404 => "No encontrado",
                            500 => "Error interno",
                            _ => "Resultado"
                        };
                    }
                }
                catch
                {
                    mensaje = "Error al procesar la respuesta";
                }

                var response = new ApiRespuesta<object>
                {
                    Exitoso = !esErrorValidacion && context.Response.StatusCode is >= 200 and < 300,
                    Mensaje = mensaje,
                    Datos = esErrorValidacion ? null : originalData,
                    Errores = errores ?? new List<string>(),
                };

                var wrapped = JsonSerializer.Serialize(response);

                context.Response.Body = originalBody;
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(wrapped);
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
            _logger.LogError(ex, "Error inesperado en middleware");

            context.Response.Body = originalBody;
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var error = JsonSerializer.Serialize(new ApiRespuesta<object>
            {
                Exitoso = false,
                Mensaje = "Error inesperado",
                Errores = new List<string> { ex.Message },
            });

            await context.Response.WriteAsync(error);
        }
    }
}
