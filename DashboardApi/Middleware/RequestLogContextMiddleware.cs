using Serilog.Context;

namespace DashboardApi;

/// <summary>
///     Middleware to log request start and end with request id.
/// </summary>
public class RequestLogContextMiddleware : IMiddleware
{
    private readonly ILogger<RequestLogContextMiddleware> _logger;

    /// <summary>
    ///    Contrustor for RequestLogContextMiddleware
    /// </summary>
    public RequestLogContextMiddleware(ILogger<RequestLogContextMiddleware> logger)
    {
        _logger = logger;
    }


    /// <summary>
    /// Adds CorrelationId to the log context 
    /// </summary>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            _logger.LogInformation("TraceIdentifier: {TraceIdentifier}", context.TraceIdentifier);
            await next(context);
        }
    }
}
