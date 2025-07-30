using MediatR;

namespace MusiciansBlog.Application.Events
{
    /// <summary>
    /// Событие по удалению блога.
    /// </summary>
    public class BlogDeletedEvent : INotification
    {
        private Guid blogId;
        public BlogDeletedEvent(Guid blogId)
        {
            this.blogId = blogId;
        }

        public Guid BlogId
        {
            get => blogId;
        }
    }
}
