namespace ApiPuertasAbiertas.API.Middleware;

public static class StandardResponseMiddlewareExtensions
{
  public static IApplicationBuilder UseStandardResponseWrapper(this IApplicationBuilder app)
  {
    return app.UseMiddleware<StandardResponseMiddleware>();
  }
}
