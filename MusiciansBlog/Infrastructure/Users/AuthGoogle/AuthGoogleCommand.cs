using MediatR;

namespace MusiciansBlog.API.Infrastructure.Users.AuthGoogle
{
    public class AuthGoogleCommand : IRequest
    {
        public string? IdToken { get; set; }
    }
}
