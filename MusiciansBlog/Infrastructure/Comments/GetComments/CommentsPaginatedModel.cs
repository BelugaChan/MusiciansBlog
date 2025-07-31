using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Comments.GetComments
{
    public class CommentsPaginatedModel : PaginatedModel
    {
        public Guid BlogId { get; set; }
    }
}
