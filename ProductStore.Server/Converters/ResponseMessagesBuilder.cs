using FluentValidation.Results;
using ProductStore.Server.Contracts;

namespace ProductStore.Server.Converters
{
    public class ResponseMessagesBuilder : IResponseMessagesBuilder
    {
        public IEnumerable<ResponseMessage> Build(IEnumerable<ValidationFailure> failures)
        {
            var errorGroups = failures.GroupBy(x => x.ErrorMessage);
            foreach (var errorGroup in errorGroups)
            {
                var code = errorGroup.Key;
                var message = new ResponseMessage()
                {
                    Code = code,
                    Reason = code,
                    Severity = "Error",
                    Parameters = errorGroup.Select(x => x.PropertyName).ToArray()
                };
                yield return message;
            }
        }
    }
}
