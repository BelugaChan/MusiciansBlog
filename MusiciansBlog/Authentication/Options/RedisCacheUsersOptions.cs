namespace MusiciansBlog.API.Authentication.Options
{
    public class RedisCacheUsersOptions
    {
        public string StaticKey { get; set; }

        public int MinutesToLive { get; set; }
    }
}
