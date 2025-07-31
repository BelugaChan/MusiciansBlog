using Mappify;
using MediatR;
using MusiciansBlog.API.Infrastructure.Comments.Common;

namespace MusiciansBlog.API.Infrastructure.Comments.AddOrUpdateComment
{
    public class AddOrUpdateCommentHandler : IRequestHandler<AddOrUpdateCommentCommand>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMappify _mapper;
        public AddOrUpdateCommentHandler(ICommentsRepository commentsRepository, IMappify mapper)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
        }
        public async Task Handle(AddOrUpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<CommentModel>(request);

            await _commentsRepository.CreateOrUpdateCommentAsync(model, cancellationToken);
        }
    }
}
