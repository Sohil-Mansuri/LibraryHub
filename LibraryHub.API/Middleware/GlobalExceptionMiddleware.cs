
using LibraryHub.API.Model;
using System.Net;
using System.Text.Json;

namespace LibraryHub.API.Middleware
{
    public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IMiddleware
    {
        private readonly JsonSerializerOptions jsonOption = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = "500"
                };

                var response = ApiResponse<string>.Fail(error);
                var json = JsonSerializer.Serialize(response, jsonOption);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
