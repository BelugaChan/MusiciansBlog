using Mappify;
using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;
using MusiciansBlog.API.Infrastructure.Comments.Common;
using MusiciansBlog.API.Infrastructure.Comments.GetComments;
using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Mappers
{
    public class FromModel : BaseMappingProfile
    {
        public override void CreateMaps(IMappify mappify)
        {
            mappify.CreateMap<FilterPaginatedModel<BlogModel>, GetBlogsResponse>(src => new GetBlogsResponse
            {
                Items = src.Items,
                PageNumber = src.PageNumber,
                PageSize = src.PageSize,
                TotalCount = src.TotalCount,
            });

            mappify.CreateMap<FilterPaginatedModel<CommentModel>, GetCommentsResponse>(src => new GetCommentsResponse
            {
                Items = src.Items,
                PageNumber = src.PageNumber,
                PageSize = src.PageSize,
                TotalCount = src.TotalCount,
            });
        }
    }
}
