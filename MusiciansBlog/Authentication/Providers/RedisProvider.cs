using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Infrastructure.Users.Common;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusiciansBlog.API.Authentication.Providers
{
    public class RedisProvider : IRedisProvider
    {
        private readonly IDatabase _redisDatabase;
        private readonly RedisCacheUsersOptions _options;
        public RedisProvider(
            IConnectionMultiplexer redis,
            IOptions<RedisCacheUsersOptions> options)
        {
            _redisDatabase = redis.GetDatabase();
            _options = options.Value;
        }

        /// <inheritdoc />
        public async Task<T?> GetStringByKeyAsync<T>(string dynamicKey)
            where T : class
        {
            var cacheKey = GenerateCacheKey(dynamicKey);

            var res = await _redisDatabase.StringGetAsync(cacheKey);

            if (res.HasValue)
            {
                //cache hit
                return JsonSerializer.Deserialize<T>(res!);
            }

            return default;
        }

        /// <inheritdoc />
        public async Task SetStringAsync<T>(string dynamicKey, T value)
            where T : class
        {
            var cacheKey = GenerateCacheKey(dynamicKey);

            await _redisDatabase.StringSetAsync(cacheKey, JsonSerializer.Serialize(value), TimeSpan.FromMinutes(_options.MinutesToLive));
        }

        /// <summary>
        /// Совмещение уникальной динамической и статической составляющей при создании ключа для сохранения сущности в Redis
        /// </summary>
        /// <param name="dynamicKey"></param>
        /// <returns></returns>
        private string GenerateCacheKey(string dynamicKey)
        {
            return $"{_options.StaticKey}:{dynamicKey}";
        }
    }
}
