using StackExchange.Redis;

namespace MusiciansBlog.API.Authentication.Providers
{
    public interface IRedisProvider
    {
        Task<T?> GetStringByKeyAsync<T>(string dynamicKey) 
            where T : class;

        Task SetStringAsync<T>(string dynamicKey, T value)
            where T : class;
    }
}
