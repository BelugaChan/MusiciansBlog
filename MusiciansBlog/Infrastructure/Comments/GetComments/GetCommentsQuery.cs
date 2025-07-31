using MediatR;
using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Comments.GetComments
{
    public class GetCommentsQuery : PaginatedModel, IRequest<GetCommentsResponse>
    {
        public Guid BlogId { get; set; }
    }
}
