using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Infrastructure.Users.AuthGoogle;
using MusiciansBlog.API.Infrastructure.Users.LoginUser;
using MusiciansBlog.API.Infrastructure.Users.RefreshTokens;
using MusiciansBlog.API.Infrastructure.Users.RegisterUser;
using System.Diagnostics.Metrics;

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

        private readonly Counter<long> _apiCallsCounter;

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

            var apiCallsMeter = new Meter("myapp.api");
            _apiCallsCounter = apiCallsMeter.CreateCounter<long>("calls.count");
        }

        /// <summary>
        /// Логин.
        /// </summary>
        /// <param name="command">Данные, вводимые при логине.</param>
        /// <param name="cancellationToken">Токен завершения работы.</param>
        /// <returns>Токен доступа и Refresh токен.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Объект не найден.</response>
        /// <response code="409">
        /// <para>объект уже существует.</para>
        /// <para>Некорректный refresh токен, либо истечение времени жизни refresh токена.</para>
        /// </response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            
            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:AccessToken").Value!, 
                result.AccessToken, 
                DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes));

            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:RefreshToken").Value!,
                result.RefreshToken,
                result.RefreshTokenExpiry);

            return Ok(result);
        }

        /// <summary>
        /// Регистрация.
        /// </summary>
        /// <param name="command">Данные, вводимые при регистрации.</param>
        /// <param name="cancellationToken">Токен завершения работы.</param>
        /// <returns>Токен доступа и Refresh токен.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Объект не найден.</response>
        /// <response code="409">
        /// <para>объект уже существует.</para>
        /// <para>Некорректный refresh токен, либо истечение времени жизни refresh токена.</para>
        /// </response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:AccessToken").Value!,
                result.AccessToken,
                DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes));

            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:RefreshToken").Value!,
                result.RefreshToken,
                result.RefreshTokenExpiry);

            return Ok(result);
        }

        /// <summary>
        /// Обновление JWT.
        /// </summary>
        /// <param name="command">Данные, получаемые при обновлении токена доступа.</param>
        /// <param name="cancellationToken">Токен завершения работы.</param>
        /// <returns></returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Объект не найден.</response>
        /// <response code="409">
        /// <para>объект уже существует.</para>
        /// <para>Некорректный refresh токен, либо истечение времени жизни refresh токена.</para>
        /// </response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Refresh(
            [FromBody] RefreshTokenCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:AccessToken").Value!,
                result.AccessToken,
                DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes));

            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:RefreshToken").Value!,
                result.RefreshToken,
                result.RefreshTokenExpiry);

            return Ok(result);
        }

        /// <summary>
        /// Аутентификация через Google.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Объект не найден.</response>
        /// <response code="409">
        /// <para>объект уже существует.</para>
        /// <para>Некорректный refresh токен, либо истечение времени жизни refresh токена.</para>
        /// </response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("google")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GoogleAuth(
            [FromBody] AuthGoogleCommand command, 
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:AccessToken").Value!,
                result.AccessToken,
                DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes));

            AddTokenToCookie(
                _configuration.GetSection("CookieKeys:RefreshToken").Value!,
                result.RefreshToken,
                result.RefreshTokenExpiry);

            return Ok(result);
        }

        private void AddTokenToCookie(string key, string token, DateTime expires)
        {
            _cookieProvider.AppendTokenToCookie(
                key,
                token,
                expires);
        }
    }
}
