using Mappify;
using MediatR;
using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Comments.Common;

namespace MusiciansBlog.API.Infrastructure.Comments.GetComments
{
    public class GetCommentsHandler : IRequestHandler<GetCommentsQuery, GetCommentsResponse>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMappify _mapper;

        public GetCommentsHandler(ICommentsRepository commentsRepository, IMappify mapper)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
        }
        public async Task<GetCommentsResponse> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<CommentsPaginatedModel>(request);

            var result = await _commentsRepository.GetBlogCommentsAsync(model, cancellationToken);

            var response = _mapper.Map<GetCommentsResponse>(result);

            return response;
        }
    }
}
