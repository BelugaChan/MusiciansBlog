using Mappify;
using MediatR;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Hashers;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Exceptions;
using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Infrastructure.Users.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMappify _mapper;
        private readonly IJWTProvider _provider;
        private readonly IPasswordHasher _hasher;
        private readonly JwtOptions _options;

        public LoginUserHandler(
            IUsersRepository usersRepository,
            IMappify mapper,
            IJWTProvider provider,
            IPasswordHasher hasher,
            IOptions<JwtOptions> options)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _provider = provider;
            _hasher = hasher;
            _options = options.Value;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var existingUser = await _usersRepository.GetByEmailAsync(request.Email, cancellationToken);
            
            if (existingUser is null)
            {
                throw new EntityNotFoundException();
            }

            var isVerifiedPassword = _hasher.VerifyPassword(request.Password, existingUser.PasswordHash);
            if (!isVerifiedPassword)
            {
                throw new UserUnauthorizedException();
            }

            //var model = _mapper.Map<UserModel>(request);

            var refreshToken = _provider.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes);

            var isOk = await _usersRepository.UpdateAsync(
                existingUser.UserId,
                user =>
                {
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiry = refreshTokenExpiry;
                }, cancellationToken);

            if (!isOk)
            {
                throw new DbUpdateException();
            }

            return new LoginUserResponse
            {
                UserId = existingUser.UserId,
                AccessToken = _provider.GenerateToken(existingUser),
                RefreshToken = refreshToken,
                RefreshTokenExpiry = refreshTokenExpiry
            };
        }
    }
}
