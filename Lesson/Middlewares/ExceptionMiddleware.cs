using TestingPlatform.Infrastructure.Exceptions;

namespace Lesson.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Exception: {Message}", exception.Message);
                var errorCodeResponse = exception switch
                {
                    InvalidOperationException exc => new CodeAndMessage(StatusCodes.Status400BadRequest, exc.Message),
                    ArgumentException exc => new CodeAndMessage(StatusCodes.Status400BadRequest, exc.Message),
                    EntityNotFoundException exc => new CodeAndMessage(StatusCodes.Status404NotFound, exc.Message),
                    _ => new CodeAndMessage(StatusCodes.Status500InternalServerError, "Internal server error")
                };
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, CodeAndMessage errorCodeResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCodeResponse.HttpStatusCode;

            await context.Response.WriteAsJsonAsync(new { mesage = errorCodeResponse.message });

        }

        private record struct CodeAndMessage(int HttpStatusCode, string message);
    }
}
