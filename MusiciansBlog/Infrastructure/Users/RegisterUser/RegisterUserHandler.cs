using Mappify;
using MediatR;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Exceptions;
using MusiciansBlog.API.Infrastructure.Users.Common;
using MusiciansBlog.API.Infrastructure.Users.Hashers;
using MusiciansBlog.API.Infrastructure.Users.Options;
using MusiciansBlog.API.Infrastructure.Users.Providers;

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

        //FIX JWT Token assignment
        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _usersRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (existingEntity is null)
            {
                var model = _mappify.Map<UserModel>(request);

                var hashedPassword = _passwordHasher.Generate(request.Password);
                var refreshToken = _provider.GenerateRefreshToken();

                model.RefreshToken = refreshToken;
                model.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiryDays);

                await _usersRepository.AddAsync(model, cancellationToken);

                return new RegisterUserResponse() 
                {
                    AccessToken = string.Empty,//FIX
                    RefreshToken = model.RefreshToken,
                    UserId = model.UserId
                };
            }
            throw new EntityAlreadyExistsException();

        }
    }
}
