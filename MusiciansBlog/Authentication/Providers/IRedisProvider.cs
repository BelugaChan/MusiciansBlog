using StackExchange.Redis;

namespace MusiciansBlog.API.Authentication.Providers
{
    /// <summary>
    /// Провайдер по работе с No-sql базой данной Redis
    /// </summary>
    public interface IRedisProvider
    {
        /// <summary>
        /// Получить сущность по ключу.
        /// </summary>
        /// <typeparam name="T">тип сущности-значения(value)</typeparam>
        /// <param name="dynamicKey">уникальная составляющая ключа у сущности</param>
        /// <returns></returns>
        Task<T?> GetStringByKeyAsync<T>(string dynamicKey) 
            where T : class;

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <typeparam name="T">тип сущности, сохраняемой в Redis</typeparam>
        /// <param name="dynamicKey">динамическая составляющая ключа</param>
        /// <param name="value">сущность, которая будет сериализована</param>
        /// <returns></returns>
        Task SetStringAsync<T>(string dynamicKey, T value)
            where T : class;
    }
}
