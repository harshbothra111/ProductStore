using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Server.Converters;

namespace ProductStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController() : ControllerBase
    {
        private ILogger? _logger;

        protected ILogger Logger
        {
            get
            {
                return _logger ??= HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(GetType());
            }
        }
        protected IResponseMessagesBuilder ResponseMessagesBuilder
            => HttpContext.RequestServices.GetRequiredService<IResponseMessagesBuilder>();
        protected IActionResult BuildBadRequestResponse(ValidationResult validationResult)
        {
            return new BadRequestObjectResult(ResponseMessagesBuilder.Build(validationResult.Errors));
        }
    }
}
