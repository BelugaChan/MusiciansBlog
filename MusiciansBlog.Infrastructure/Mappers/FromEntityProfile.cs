using Mappify;
using MusiciansBlog.Application.Models;
using MusiciansBlog.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusiciansBlog.Infrastructure.Mappers
{
    public sealed class FromEntityProfile : BaseMappingProfile
    {
        public override void CreateMaps(IMappify mappify)
        {
            mappify.CreateMap<BlogEntity, Blog>(src => new Blog
            {
                BlogId = src.BlogId,
                Description = src.Description,
                DislikeCount = src.DislikeCount,
                AuthorId = src.AuthorId,
                Created = src.Created,
                LikeCount = src.LikeCount,
                Title = src.Title,
                Updated = src.Updated,
            });

            mappify.CreateMap<CommentEntity, Comment>(src => new Comment
            {
                CommentId = src.CommentId,
                DislikeCount = src.DislikeCount,
                AuthorId = src.AuthorId,
                Content = src.Content,
                Created = src.Created,
                LikeCount = src.LikeCount,
                ParentBlogId = src.ParentBlogId,
                Updated = src.Updated
            });

            mappify.CreateMap<UserEntity, User>(src => new User
            {
                UserId = src.UserId,
                Created = src.Created,
                Email = src.Email,
                PasswordHash = src.PasswordHash,
                RefreshToken = src.RefreshToken,
                RefreshTokenExpiry = src.RefreshTokenExpiry,
                Username = src.Username
            });
        }
    }
}
