using Mappify;
using Microsoft.EntityFrameworkCore;
using MusiciansBlog.API.Exceptions;
using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;
using MusiciansBlog.API.Infrastructure.Comments.DeleteComment;
using MusiciansBlog.API.Infrastructure.Comments.GetComments;
using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Comments.Common
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public class CommentsRepository : ICommentsRepository
    {
        private readonly MyDbContext _dbContext;
        private readonly IMappify _mapper;
        public CommentsRepository(MyDbContext dbContext, IMappify mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task CreateOrUpdateCommentAsync(CommentModel model, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Comments.FindAsync(model.CommentId, ct);

            var entity = _mapper.Map<CommentEntity>(model);

            if (existingEntity is not null)
            {
                entity.Updated = DateTimeOffset.UtcNow;
                _dbContext.Comments.Update(entity);
            }
            else
            {
                entity.Created = DateTimeOffset.UtcNow;
                await _dbContext.Comments.AddAsync(entity, ct);
            }

            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task DeleteCommentAsync(DeleteCommentModel model, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Comments.FindAsync(model.CommentId, ct);

            if (existingEntity is null)
            {
                throw new EntityNotFoundException();
            }

            existingEntity.IsDeleted = true;

            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task<FilterPaginatedModel<CommentModel>> GetBlogCommentsAsync(CommentsPaginatedModel model, CancellationToken ct)
        {
            var entities = _dbContext.Comments
                .AsNoTracking()
                .Where(c => c.ParentBlogId == model.BlogId)
                .AsQueryable();


            var entitiesCount = await entities.CountAsync(ct);

            var pagedEntities = await entities.Skip(model.PageNumber * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync(ct);

            var comments = _mapper.MapList<CommentModel>(pagedEntities);

            return new FilterPaginatedModel<CommentModel>
            {
                Items = comments,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalCount = entitiesCount
            };
        }
    }
}
