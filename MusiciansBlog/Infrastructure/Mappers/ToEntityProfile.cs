using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mappify;
using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Comments.Common;
using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Infrastructure.Mappers
{
    public sealed class ToEntityProfile : BaseMappingProfile
    {
        public override void CreateMaps(IMappify mappify)
        {
            mappify.CreateMap<BlogModel, BlogEntity>(src => new BlogEntity
            {
                BlogId = src.BlogId,
                AuthorId = src.AuthorId,
                Created = src.Created,
                Description = src.Description,
                DislikeCount = src.DislikeCount,
                LikeCount = src.LikeCount,
                Title = src.Title
            });

            mappify.CreateMap<CommentModel, CommentEntity>(src => new CommentEntity
            {
                CommentId = src.CommentId,
                Content = src.Content,
                Created = src.Created,
                DislikeCount = src.DislikeCount,
                AuthorId = src.AuthorId,
                LikeCount = src.LikeCount,
                ParentBlogId = src.ParentBlogId,
            });

            mappify.CreateMap<UserModel, UserEntity>(src => new UserEntity
            {
                UserId = src.UserId,
                Created = src.Created,
                Email = src.Email,
                PasswordHash = src.PasswordHash,
                Username = src.Username,
                RefreshToken = src.RefreshToken,
                RefreshTokenExpiry = src.RefreshTokenExpiry
            });
        }
    }
}
