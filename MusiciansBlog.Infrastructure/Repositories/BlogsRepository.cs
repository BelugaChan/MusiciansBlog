using Mappify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MusiciansBlog.Application.Models;
using MusiciansBlog.Logic.Entities;
using MusiciansBlog.Logic.Exceptions;
using MusiciansBlog.Application.Events;
using MusiciansBlog.Application.Repositories;
using MusiciansBlog.Infrastructure.Data;

namespace MusiciansBlog.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public sealed class BlogsRepository : IBlogsRepository
    {
        private readonly IMediator _mediator;
        private readonly IMappify _mapper;
        private readonly MyDbContext _dbContext;
        public BlogsRepository(IMediator mediator, IMappify mapper, MyDbContext dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task CreateOrUpdateBlogAsync(Blog model, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Blogs.FindAsync(model.BlogId, ct);

            var entity = _mapper.Map<BlogEntity>(model);

            if (existingEntity is not null)
            {
                _dbContext.Blogs.Update(entity);
            }
            else
            {
                await _dbContext.Blogs.AddAsync(entity, ct);
            }
            
            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task DeleteBlogAsync(Guid blogId, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Blogs.FindAsync(blogId, ct);

            if (existingEntity is null)
            {
                throw new EntityNotFoundException();
            }

            existingEntity.IsDeleted = true;

            await _mediator.Publish(new BlogDeletedEvent(blogId), ct);

            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task<FilterPaginatedModel<Blog>> GetBlogsAsync(BlogPaginatedModel model, CancellationToken ct)
        {
            var entities = await _dbContext.Blogs.
                AsNoTracking()
                .Skip(model.PageNumber * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync(ct);

            var totalCount = await _dbContext.Blogs.CountAsync(ct);

            var blogs = _mapper.MapList<Blog>(entities);
            
            return new FilterPaginatedModel<Blog>
            {
                Items = blogs, 
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
