
namespace DashboardApi;

///<summary>
/// Global error handling middleware
///</summary>
public class GlobalErrorHandlingMiddeware : IMiddleware
{
    private readonly ILogger<GlobalErrorHandlingMiddeware> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public GlobalErrorHandlingMiddeware(ILogger<GlobalErrorHandlingMiddeware> logger)
    {
        _logger = logger;
    }

    ///
    ///<summary>
    /// Catches all unhandled exceptions and logs them
    ///</summary>
    ///
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
