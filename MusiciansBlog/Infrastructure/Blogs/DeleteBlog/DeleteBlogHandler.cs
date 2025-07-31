using Mappify;
using MediatR;
using MusiciansBlog.API.Infrastructure.Blogs.Common;

namespace MusiciansBlog.API.Infrastructure.Blogs.DeleteBlog
{
    public class DeleteBlogHandler : IRequestHandler<DeleteBlogCommand>
    {
        private readonly IBlogsRepository _blogsRepository;
        private readonly IMappify _mapper;

        public DeleteBlogHandler(IBlogsRepository blogsRepository, IMappify mapper)
        {
            _blogsRepository = blogsRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DeleteBlogModel>(request);

            await _blogsRepository.DeleteBlogAsync(model, cancellationToken);
        }
    }
}
