using ProductStore.Infrastructure.Common.Exceptions;

namespace ProductStore.Infrastructure.Data.Exceptions
{
    public class DatabaseException(string message, Exception? innerException = null) : InfrastructureException(message, innerException)
    {
    }
}
