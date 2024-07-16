namespace ProductStore.Domain
{
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; set; } = [];

        public bool HasPrevious => CurrentPage > 0;
        public bool HasNext => CurrentPage + 1 < TotalPages;
    }
}
