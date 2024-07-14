using System.Text.Json.Serialization;

namespace ProductStore.Domain.AggregateModels.ProductAggregate
{
    public class SubCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = [];
    }
}
