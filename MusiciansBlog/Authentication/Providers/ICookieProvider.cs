namespace MusiciansBlog.API.Authentication.Providers
{
    public interface ICookieProvider
    {
        void AppendTokenToCookie(string key, string tokenValue, DateTime tokenExpiry);
    }
}
