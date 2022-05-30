using System.Text.Json;
using SpaceFlightNews.Data.Models;

namespace SpaceFlightNews.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _accessor;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, IHttpContextAccessor accessor, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _accessor = accessor;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error was ocurred during the request {_accessor.HttpContext?.TraceIdentifier}");
            if (_accessor.HttpContext != null)
            {
                int statusCode = StatusCodes.Status500InternalServerError;
                await HandleExceptionAsync(_accessor.HttpContext.Response, statusCode, new() { e.Message });
            }
        }
    }

    private Task HandleExceptionAsync(HttpResponse response, int statusCode, List<string> errors)
    {
        response.StatusCode = statusCode;
        response.ContentType = "application/json";

        var apiResponse = JsonSerializer.Serialize(new ApiResponse<dynamic>()
        {
            Result = null,
            Errors = errors,
            ElapsedTimeInMilliseconds = 0
        }, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        return response.WriteAsync(apiResponse);
    }
}