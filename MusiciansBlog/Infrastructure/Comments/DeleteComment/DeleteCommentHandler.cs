using Mappify;
using MediatR;
using MusiciansBlog.API.Infrastructure.Comments.Common;

namespace MusiciansBlog.API.Infrastructure.Comments.DeleteComment
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMappify _mapper;
        public DeleteCommentHandler(ICommentsRepository commentsRepository, IMappify mapper)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DeleteCommentModel>(request);

            await _commentsRepository.DeleteCommentAsync(model, cancellationToken);
        }
    }
}
