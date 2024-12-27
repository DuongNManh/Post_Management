using Post_Management.API.Models;
using Post_Management.API.Exceptions;
using System.Net;

namespace Post_Management.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        // Fields
        private readonly ILogger<ExceptionHandlerMiddleware> _logger; // for logging
        private readonly RequestDelegate _next; // for the next middleware
        private readonly IHostEnvironment _env; // for

        //constructor
        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _env = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var errorId = Guid.NewGuid().ToString();
                LogError(errorId, exception);
                await HandleExceptionAsync(context, errorId, exception);
            }
        }

        private void LogError(string errorId, Exception exception)
        {
            var error = new
            {
                ErrorId = errorId,
                ExceptionType = exception.GetType().Name,
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace
            };

            _logger.LogError(exception, "{@error}", error);
        }

        private async Task HandleExceptionAsync(HttpContext context, string errorId, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var message = GetMessage(exception);
            var reason = GetReason(exception);

            var response = new ApiResponse<object>
            {
                StatusCode = (int)statusCode,
                Message = message,
                Reason = reason,
                IsSuccess = false,
                Data = new
                {
                    ErrorId = errorId,
                    Timestamp = DateTime.UtcNow
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private string GetReason(Exception exception)
        {
            return exception switch
            {
                ApiException apiException => $"Error occurred in {apiException.GetType().Name.Replace("Exception", "")}",
                UnauthorizedAccessException => "Unauthorized access to resource",
                KeyNotFoundException => "Requested resource was not found",
                ArgumentException => "Invalid argument provided to operation",
                InvalidOperationException => "Invalid operation attempted",
                _ => _env.IsDevelopment() ? exception.Message : "Internal Server Error"
            };
        }

        private static string GetMessage(Exception exception)
        {
            return exception switch
            {
                ApiException apiException => apiException.Message,
                UnauthorizedAccessException => "Unauthorized access",
                KeyNotFoundException => "Resource not found",
                ArgumentException => "Invalid argument provided",
                InvalidOperationException => "Invalid operation",
                _ => "An unexpected error occurred"
            };
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
        {
            return exception switch
            {
                ApiException apiException => apiException.StatusCode,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                ArgumentException => HttpStatusCode.BadRequest,
                InvalidOperationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }

}

