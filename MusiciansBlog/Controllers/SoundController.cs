using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusiciansBlog.API.Infrastructure.Sounds.AddSound;

namespace MusiciansBlog.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SoundController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SoundController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Распознавание ноты по массиву байтов сырых данных
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Ok.</response>
        /// <returns></returns>
        [HttpPost("recognize")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RecognizeNote([FromBody] AddSoundCommand command)
        {
            var res = await _mediator.Send(command);

            return Ok(res);
        }
    }
}
