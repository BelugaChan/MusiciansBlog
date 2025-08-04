using Mappify;
using MediatR;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Hashers;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Exceptions;
using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Infrastructure.Users.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IMappify _mappify;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJWTProvider _provider;
        private readonly JwtOptions _options;

        public RegisterUserHandler(
            IMappify mappify, 
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJWTProvider provider,
            IOptions<JwtOptions> options
            )
        {
            _mappify = mappify;
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _provider = provider;
            _options = options.Value;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUserAlreadyExists =
                await _usersRepository
                    .CheckUserExistence(request.Username, request.Email, cancellationToken);

            if (isUserAlreadyExists)
            {
                throw new EntityAlreadyExistsException();
            }

            var model = _mappify.Map<UserModel>(request);

            var hashedPassword = _passwordHasher.Generate(request.Password);
            var refreshToken = _provider.GenerateRefreshToken();
            var accessToken = _provider.GenerateToken(model); 
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiryDays);

            model.RefreshToken = refreshToken;
            model.RefreshTokenExpiry = refreshTokenExpiry;
            model.PasswordHash = hashedPassword;

            await _usersRepository.AddAsync(model, cancellationToken);

            return new RegisterUserResponse
            {
                AccessToken = accessToken,
                RefreshToken = model.RefreshToken,
                RefreshTokenExpiry = refreshTokenExpiry,
                UserId = model.UserId
            };

        }
    }
}
