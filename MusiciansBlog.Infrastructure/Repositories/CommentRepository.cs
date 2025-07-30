using Mappify;
using Microsoft.EntityFrameworkCore;
using MusiciansBlog.Application.Models;
using MusiciansBlog.Application.Repositories;
using MusiciansBlog.Infrastructure.Data;
using MusiciansBlog.Logic.Entities;
using MusiciansBlog.Logic.Exceptions;

namespace MusiciansBlog.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public class CommentRepository : ICommentsRepository
    {
        private readonly MyDbContext _dbContext;
        private readonly IMappify _mapper;
        public CommentRepository(MyDbContext dbContext, IMappify mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task CreateOrUpdateBlogAsync(Comment model, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Comments.FindAsync(model.CommentId, ct);

            var entity = _mapper.Map<CommentEntity>(model);

            if (existingEntity is not null)
            {
                _dbContext.Comments.Update(entity);
            }
            else
            {
                await _dbContext.Comments.AddAsync(entity, ct);
            }

            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task DeleteCommentAsync(Guid commentId, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Comments.FindAsync(commentId, ct);

            if (existingEntity is null)
            {
                throw new EntityNotFoundException();
            }

            existingEntity.IsDeleted = true;

            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task<FilterPaginatedModel<Comment>> GetBlogCommentsAsync(BlogPaginatedModel model, CancellationToken ct)
        {
            var entities = _dbContext.Comments
                .AsNoTracking()
                .Where(c => c.ParentBlogId == model.BlogId)
                .AsQueryable();


            var entitiesCount = await entities.CountAsync(ct);

            var pagedEntities = await entities.Skip(model.PageNumber * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync(ct);

            var comments = _mapper.MapList<Comment>(pagedEntities);

            return new FilterPaginatedModel<Comment>
            {
                Items = comments,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalCount = entitiesCount
            };
        }
    }
}
