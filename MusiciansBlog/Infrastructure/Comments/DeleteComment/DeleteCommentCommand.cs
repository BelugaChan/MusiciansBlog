using MediatR;

namespace MusiciansBlog.API.Infrastructure.Comments.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }
    }
}
