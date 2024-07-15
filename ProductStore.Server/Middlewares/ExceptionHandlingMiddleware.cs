
using ProductStore.Server.Contracts;

namespace ProductStore.Server.Middlewares
{
    public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
                logger.LogError(ex, "Exception occured. Message: {message}", ex.Message);
                var message = new ResponseMessage()
                {
                    Code = "0000",
                    Reason = ex.Message,
                    Severity = "Fatal"
                };
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(message);
			}
        }
    }
}
