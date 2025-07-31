namespace MusiciansBlog.API.Infrastructure.Users.Options
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;

        public int AccessTokenExpiryMinutes { get; set; }

        public int RefreshTokenExpiryDays { get; set; }
    }
}
