using Mappify;
using MediatR;
using MusiciansBlog.API.Infrastructure.Blogs.Common;

namespace MusiciansBlog.API.Infrastructure.Blogs.AddOrUpdateBlog
{
    public class AddOrUpdateBlogHandler : IRequestHandler<AddOrUpdateBlogCommand>
    {
        private readonly IBlogsRepository _blogsRepository;
        private readonly IMappify _mapper;

        public AddOrUpdateBlogHandler(IBlogsRepository blogsRepository, IMappify mapper)
        {
            _blogsRepository = blogsRepository;
            _mapper = mapper;
        }
        public async Task Handle(AddOrUpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<BlogModel>(request);

            await _blogsRepository.CreateOrUpdateBlogAsync(model, cancellationToken);
        }
    }
}
