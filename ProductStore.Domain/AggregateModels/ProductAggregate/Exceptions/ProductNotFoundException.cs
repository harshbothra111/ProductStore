namespace ProductStore.Domain.AggregateModels.ProductAggregate.Exceptions
{
    public class ProductNotFoundException(string message) : Exception(message)
    {
    }
}
