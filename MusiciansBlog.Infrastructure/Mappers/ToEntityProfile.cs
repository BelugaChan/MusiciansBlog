using MusiciansBlog.Application.Models;
using MusiciansBlog.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mappify;

namespace MusiciansBlog.Infrastructure.Mappers
{
    public sealed class ToEntityProfile : BaseMappingProfile
    {
        public override void CreateMaps(IMappify mappify)
        {
            mappify.CreateMap<Blog, BlogEntity>(src => new BlogEntity
            {
                BlogId = src.BlogId,
                AuthorId = src.AuthorId,
                Created = src.Created,
                Description = src.Description,
                DislikeCount = src.DislikeCount,
                LikeCount = src.LikeCount,
                Title = src.Title,
                Updated = src.Updated
            });

            mappify.CreateMap<Comment, CommentEntity>(src => new CommentEntity
            {
                CommentId = src.CommentId,
                Content = src.Content,
                Created = src.Created,
                Updated = src.Updated,
                DislikeCount = src.DislikeCount,
                AuthorId = src.AuthorId,
                LikeCount = src.LikeCount,
                ParentBlogId = src.ParentBlogId,
            });

            mappify.CreateMap<User, UserEntity>(src => new UserEntity
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
