namespace MusiciansBlog.API.Authentication.Options
{
    public class RedisCacheUsersOptions
    {
        /// <summary>
        /// Статический ключ для Redis
        /// </summary>
        public string StaticKey { get; set; }

        /// <summary>
        /// Время жизни пары ключ-значение в Redis
        /// </summary>
        public int MinutesToLive { get; set; }
    }
}
