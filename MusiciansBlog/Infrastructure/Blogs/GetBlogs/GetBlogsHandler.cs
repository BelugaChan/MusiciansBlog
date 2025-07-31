using Mappify;
using MediatR;
using MusiciansBlog.API.Infrastructure.Blogs.Common;

namespace MusiciansBlog.API.Infrastructure.Blogs.GetBlogs
{
    public class GetBlogsHandler : IRequestHandler<GetBlogsQuery, GetBlogsResponse>
    {
        private readonly IBlogsRepository _blogsRepository;
        private readonly IMappify _mapper;

        public GetBlogsHandler(IBlogsRepository blogsRepository, IMappify mapper)
        {
            _blogsRepository = blogsRepository;
            _mapper = mapper;
        }

        public async Task<GetBlogsResponse> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<BlogsPaginatedModel>(request);

            var result = await _blogsRepository.GetBlogsAsync(model, cancellationToken);

            var response = _mapper.Map<GetBlogsResponse>(result);

            return response;
        }
    }
}
