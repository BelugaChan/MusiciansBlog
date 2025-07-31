namespace MusiciansBlog.API.Infrastructure.Users.RegisterUser
{
    public class RegisterUserResponse
    {
        public required Guid UserId { get; set; }

        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
