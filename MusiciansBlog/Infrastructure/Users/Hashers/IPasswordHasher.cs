namespace MusiciansBlog.API.Infrastructure.Users.Hashers
{
    public interface IPasswordHasher
    {
        string Generate(string password);
    }
}
