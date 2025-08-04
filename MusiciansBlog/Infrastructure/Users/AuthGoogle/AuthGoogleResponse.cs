namespace MusiciansBlog.API.Infrastructure.Users.AuthGoogle
{
    public class AuthGoogleResponse
    {
        public required Guid UserId { get; set; }

        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }

        public required DateTime RefreshTokenExpiry { get; set; }
    }
}
