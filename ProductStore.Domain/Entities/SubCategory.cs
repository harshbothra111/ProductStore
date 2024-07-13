namespace ProductStore.Domain.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product> Products { get; set; } = [];
    }
}
