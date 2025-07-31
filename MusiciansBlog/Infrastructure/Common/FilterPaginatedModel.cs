namespace MusiciansBlog.API.Infrastructure.Common
{
    public class FilterPaginatedModel<T> : PaginatedModel
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }

        public int TotalPages => PageSize == 0 ? 0 : TotalCount / PageSize + Math.Min(1, TotalCount % PageSize);
    }
}
