using Mappify;
using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Comments.Common;
using MusiciansBlog.API.Infrastructure.Users.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusiciansBlog.API.Infrastructure.Mappers
{
    public sealed class FromEntityProfile : BaseMappingProfile
    {
        public override void CreateMaps(IMappify mappify)
        {
            mappify.CreateMap<BlogEntity, BlogModel>(src => new BlogModel
            {
                BlogId = src.BlogId,
                Description = src.Description,
                DislikeCount = src.DislikeCount,
                AuthorId = src.AuthorId,
                Created = src.Created,
                LikeCount = src.LikeCount,
                Title = src.Title
            });

            mappify.CreateMap<CommentEntity, CommentModel>(src => new CommentModel
            {
                CommentId = src.CommentId,
                DislikeCount = src.DislikeCount,
                AuthorId = src.AuthorId,
                Content = src.Content,
                Created = src.Created,
                LikeCount = src.LikeCount,
                ParentBlogId = src.ParentBlogId
            });

            mappify.CreateMap<UserEntity, UserModel>(src => new UserModel
            {
                UserId = src.UserId,
                Created = src.Created,
                Email = src.Email,
                PasswordHash = src.PasswordHash,
                RefreshToken = src.RefreshToken,
                RefreshTokenExpiry = src.RefreshTokenExpiry,
                Username = src.Username,
                AuthType = src.AuthType,
            });
        }
    }
}
