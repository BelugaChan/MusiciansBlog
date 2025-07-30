using MusiciansBlog.Application.Models;

namespace MusiciansBlog.Application.Repositories
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    public interface IBlogsRepository
    {
        /// <summary>
        /// Создать/обновить блог.
        /// </summary>
        /// <param name="model">Сущность.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task CreateOrUpdateBlogAsync(Blog model, CancellationToken ct);

        /// <summary>
        /// Удалить блог.
        /// </summary>
        /// <param name="blogId">Id блога.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task DeleteBlogAsync(Guid blogId, CancellationToken ct);

        /// <summary>
        /// Список.
        /// </summary>
        /// <param name="model">Фильтр (только пагинация).</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task<FilterPaginatedModel<Blog>> GetBlogsAsync(BlogPaginatedModel model, CancellationToken ct);
    }
}
