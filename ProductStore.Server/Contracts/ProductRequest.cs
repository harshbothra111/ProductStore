namespace ProductStore.Server.Contracts
{
    public class ProductRequest
    {
        public required string Product { get; set; }
        public required IFormFile? File { get; set; }
    }
}
