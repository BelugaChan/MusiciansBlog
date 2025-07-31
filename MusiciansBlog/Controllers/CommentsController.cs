using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusiciansBlog.API.Infrastructure.Comments.AddOrUpdateComment;
using MusiciansBlog.API.Infrastructure.Comments.DeleteComment;
using MusiciansBlog.API.Infrastructure.Comments.GetComments;

namespace MusiciansBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBlog(
            [FromBody] AddOrUpdateCommentCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBlog(
            [FromBody] DeleteCommentCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs(
            [FromBody] GetCommentsQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }
    }
}
