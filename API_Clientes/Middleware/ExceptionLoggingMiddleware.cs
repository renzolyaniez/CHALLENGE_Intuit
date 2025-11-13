using System.Text.Json;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Model;
using Model.Model;

namespace API_Clientes.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLoggingMiddleware> _logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.TraceIdentifier;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception (CorrelationId: {CorrelationId})", correlationId);

                // Guardar en base de datos usando un scope de servicios
                try
                {
                    using var scope = context.RequestServices.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<Contexto>();

                    var errorLog = new ErrorLog
                    {
                        Timestamp = DateTime.UtcNow,
                        Level = "Error",
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Path = context.Request?.Path,
                        QueryString = context.Request?.QueryString.ToString(),
                        User = context.User?.Identity?.Name,
                        InnerException = ex.InnerException?.Message,
                        AdditionalData = BuildAdditionalData(context),
                        CorrelationId = correlationId
                    };

                    db.ErrorLogs.Add(errorLog);
                    await db.SaveChangesAsync();
                }
                catch (Exception dbEx)
                {
                    // Si falla guardar en BD no queremos tirar otro error; lo logueamos localmente.
                    _logger.LogError(dbEx, "Error al guardar ErrorLog (CorrelationId: {CorrelationId})", correlationId);
                }

                // Respuesta genérica para API
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var payload = new
                {
                    error = "Ocurrió un error interno en el servidor.",
                    correlationId = correlationId
                };

                var json = JsonSerializer.Serialize(payload);
                await context.Response.WriteAsync(json);
            }
        }

        private string BuildAdditionalData(HttpContext context)
        {
            try
            {
                // Añadir cabeceras relevantes y método. Evitar incluir cuerpos grandes o sensibles.
                var info = new
                {
                    Method = context.Request?.Method,
                    Headers = context.Request?.Headers
                        .Where(h => !string.Equals(h.Key, "Authorization", StringComparison.OrdinalIgnoreCase))
                        .ToDictionary(h => h.Key, h => string.Join(",", h.Value.ToArray())),
                    Host = context.Request?.Host.Value
                };

                return JsonSerializer.Serialize(info);
            }
            catch
            {
                return null;
            }
        }
    }

    // Extensión para registrar middleware de forma limpia
    public static class ExceptionLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}