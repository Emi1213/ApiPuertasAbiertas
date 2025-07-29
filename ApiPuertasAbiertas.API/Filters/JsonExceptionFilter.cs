using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ApiPuertasAbiertas.Shared.Responses;
using System.Text.Json;

namespace ApiPuertasAbiertas.API.Filters;

public class JsonExceptionFilter : IExceptionFilter
{
    private readonly ILogger<JsonExceptionFilter> _logger;

    public JsonExceptionFilter(ILogger<JsonExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is JsonException jsonEx)
        {
            _logger.LogError(jsonEx, "Error de deserialización JSON");

            var response = new ApiRespuesta<object>
            {
                Exitoso = false,
                Mensaje = "Error en la estructura del JSON recibido",
                Errores = new List<string> { jsonEx.Message },
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 400
            };

            context.ExceptionHandled = true;
        }
    }
}
