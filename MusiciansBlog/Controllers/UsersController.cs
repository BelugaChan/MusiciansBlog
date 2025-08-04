using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusiciansBlog.API.Infrastructure.Users.LoginUser;
using MusiciansBlog.API.Infrastructure.Users.RegisterUser;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Infrastructure.Users.AuthGoogle;

namespace MusiciansBlog.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICookieProvider _cookieProvider;
        private readonly IConfiguration _configuration;
        private readonly JwtOptions _options;
        public UsersController(
            IMediator mediator,
            ICookieProvider cookieProvider,
            IConfiguration configuration,
            IOptions<JwtOptions> options) 
        {
            _mediator = mediator;
            _cookieProvider = cookieProvider;
            _configuration = configuration;
            _options = options.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            
            _cookieProvider.AppendTokenToCookie(
                _configuration.GetSection("CookieKeys:AccessToken").Value!, 
                result.AccessToken, 
                DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes));

            _cookieProvider.AppendTokenToCookie(
                _configuration.GetSection("CookieKeys:RefreshToken").Value!,
                result.RefreshToken,
                result.RefreshTokenExpiry);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            _cookieProvider.AppendTokenToCookie(
                _configuration.GetSection("CookieKeys:AccessToken").Value!,
                result.AccessToken,
                DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes));

            _cookieProvider.AppendTokenToCookie(
                _configuration.GetSection("CookieKeys:RefreshToken").Value!,
                result.RefreshToken,
                result.RefreshTokenExpiry);

            return Ok(result);
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleAuth(
            [FromBody] AuthGoogleCommand command, 
            CancellationToken cancellationToken)
        {

        }

    }
}
