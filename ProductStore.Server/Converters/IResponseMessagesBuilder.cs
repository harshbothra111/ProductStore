using FluentValidation.Results;
using ProductStore.Server.Contracts;

namespace ProductStore.Server.Converters
{
    public interface IResponseMessagesBuilder
    {
        IEnumerable<ResponseMessage> Build(IEnumerable<ValidationFailure> failures);
    }
}
