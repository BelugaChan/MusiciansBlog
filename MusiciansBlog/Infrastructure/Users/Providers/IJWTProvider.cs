namespace MusiciansBlog.API.Infrastructure.Users.Providers
{
    public interface IJWTProvider
    {
        string GenerateToken();

        string GenerateRefreshToken();
    }
}
