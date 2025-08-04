namespace MusiciansBlog.API.Infrastructure.Users.RefreshTokens
{
    public class RefreshTokenResponse
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }

        public required DateTime RefreshTokenExpiry { get; set; }
    }
}
