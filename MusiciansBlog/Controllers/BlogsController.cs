using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusiciansBlog.API.Infrastructure.Blogs.AddOrUpdateBlog;
using MusiciansBlog.API.Infrastructure.Blogs.DeleteBlog;
using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;

namespace MusiciansBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBlog(
            [FromBody] AddOrUpdateBlogCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBlog(
            [FromBody] DeleteBlogCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs(
            [FromBody] GetBlogsQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }
    }
}
