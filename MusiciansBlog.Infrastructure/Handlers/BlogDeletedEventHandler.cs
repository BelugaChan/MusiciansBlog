using MediatR;
using Microsoft.EntityFrameworkCore;
using MusiciansBlog.Application.Events;
using MusiciansBlog.Infrastructure.Data;

namespace MusiciansBlog.Infrastructure.Handlers
{
    /// <summary>
    /// Обработчик по удалению комментариев блога
    /// </summary>
    public class BlogDeletedEventHandler : INotificationHandler<BlogDeletedEvent>
    {
        private readonly MyDbContext _dbContext;

        public BlogDeletedEventHandler(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(BlogDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _dbContext.Comments
                .Where(c => c.ParentBlogId == notification.BlogId)
                .ExecuteUpdateAsync(i =>
                    i.SetProperty(c => c.IsDeleted, true), cancellationToken);
        }
    }
}
