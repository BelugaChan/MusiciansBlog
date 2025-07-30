namespace MusiciansBlog.Application.Models
{
    public sealed class BlogPaginatedModel : PaginatedModel
    {
        public Guid BlogId { get; set; }
    }
}
