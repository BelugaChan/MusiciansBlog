using MediatR;
using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Blogs.GetBlogs
{
    public class GetBlogsQuery : PaginatedModel, IRequest<GetBlogsResponse>
    {
    }
}
