using Mappify;
using Microsoft.EntityFrameworkCore;
using MusiciansBlog.API.Exceptions;
using MusiciansBlog.API.Infrastructure.Blogs.DeleteBlog;
using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;
using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Blogs.Common
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public sealed class BlogsRepository : IBlogsRepository
    {
        private readonly IMappify _mapper;
        private readonly MyDbContext _dbContext;
        public BlogsRepository(IMappify mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task CreateOrUpdateBlogAsync(BlogModel model, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Blogs.FindAsync(model.BlogId, ct);

            var entity = _mapper.Map<BlogEntity>(model);

            if (existingEntity is not null)
            {
                entity.Updated = DateTimeOffset.UtcNow;
                _dbContext.Blogs.Update(entity);
            }
            else
            {
                entity.Created = DateTimeOffset.UtcNow;
                await _dbContext.Blogs.AddAsync(entity, ct);
            }
            
            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task DeleteBlogAsync(DeleteBlogModel model, CancellationToken ct)
        {
            var existingEntity = await _dbContext.Blogs.FindAsync(model.BlogId, ct);

            if (existingEntity is null)
            {
                throw new EntityNotFoundException();
            }

            existingEntity.IsDeleted = true;

            await _dbContext.Comments
                .Where(c => c.ParentBlogId == existingEntity.BlogId)
                .ExecuteUpdateAsync(i => 
                    i.SetProperty(c => c.IsDeleted, true), ct);

            await _dbContext.SaveChangesAsync(ct);
        }

        /// <inheritdoc />
        public async Task<FilterPaginatedModel<BlogModel>> GetBlogsAsync(BlogsPaginatedModel model, CancellationToken ct)
        {
            var entities = await _dbContext.Blogs.
                AsNoTracking()
                .Skip(model.PageNumber * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync(ct);

            var totalCount = await _dbContext.Blogs.CountAsync(ct);

            var blogs = _mapper.MapList<BlogModel>(entities);
            
            return new FilterPaginatedModel<BlogModel>
            {
                Items = blogs, 
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
