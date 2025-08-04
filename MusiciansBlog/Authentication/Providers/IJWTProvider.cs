using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Authentication.Providers
{
    public interface IJWTProvider
    {
        string GenerateToken(UserModel model);

        string GenerateRefreshToken();
    }
}
