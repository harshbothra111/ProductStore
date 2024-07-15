namespace ProductStore.Server.Contracts
{
    public class ResponseMessage
    {
        public required string Code { get; set; }
        public required string Reason { get; set; }
        public required string Severity { get; set; }
        public IReadOnlyCollection<string> Parameters { get; init; } = [];
    }
}
