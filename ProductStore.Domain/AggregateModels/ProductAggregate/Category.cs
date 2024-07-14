using System.Text.Json.Serialization;

namespace ProductStore.Domain.AggregateModels.ProductAggregate
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = [];
    }
}
