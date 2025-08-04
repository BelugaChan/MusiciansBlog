using Mappify;
using MusiciansBlog.API.Infrastructure.Blogs.AddOrUpdateBlog;
using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Blogs.DeleteBlog;
using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;
using MusiciansBlog.API.Infrastructure.Comments.AddOrUpdateComment;
using MusiciansBlog.API.Infrastructure.Comments.Common;
using MusiciansBlog.API.Infrastructure.Comments.DeleteComment;
using MusiciansBlog.API.Infrastructure.Comments.GetComments;
using MusiciansBlog.API.Infrastructure.Users.Common;
using MusiciansBlog.API.Infrastructure.Users.RegisterUser;

namespace MusiciansBlog.API.Infrastructure.Mappers
{
    public class ToModel : BaseMappingProfile
    {
        public override void CreateMaps(IMappify mappify)
        {
            mappify.CreateMap<AddOrUpdateBlogCommand, BlogModel>(src => new BlogModel
            {
                AuthorId = src.AuthorId,
                Description = src.Description,
                DislikeCount = src.DislikeCount,
                LikeCount = src.LikeCount,
                Title = src.Title,
            });

            mappify.CreateMap<GetBlogsQuery, BlogsPaginatedModel>(src => new BlogsPaginatedModel
            {
                PageNumber = src.PageNumber,
                PageSize = src.PageSize,
            });

            mappify.CreateMap<DeleteBlogCommand, DeleteBlogModel>(src => new DeleteBlogModel
            {
                BlogId = src.BlogId,
            });

            mappify.CreateMap<AddOrUpdateCommentCommand, CommentModel>(src => new CommentModel
            {
                AuthorId = src.AuthorId,
                Content = src.Content,
                DislikeCount = src.DislikeCount,
                LikeCount = src.LikeCount,
                ParentBlogId = src.ParentBlogId,
            });

            mappify.CreateMap<DeleteCommentCommand, DeleteCommentModel>(src => new DeleteCommentModel
            {
                CommentId = src.CommentId,
            });

            mappify.CreateMap<GetCommentsQuery, CommentsPaginatedModel>(src => new CommentsPaginatedModel
            {
                BlogId = src.BlogId,
                PageNumber = src.PageNumber,
                PageSize = src.PageSize,
            });

            mappify.CreateMap<RegisterUserCommand, UserModel>(src => new UserModel
            {
                UserId = Guid.NewGuid(),
                Created = DateTimeOffset.Now,
                Email = src.Email,
                Username = src.Username,
            });

            //mappify.CreateMap<LoginUserCommand, UserModel>(_ => new UserModel
            //{
            //    UserId = Guid.NewGuid(),
            //});
        }
    }
}
