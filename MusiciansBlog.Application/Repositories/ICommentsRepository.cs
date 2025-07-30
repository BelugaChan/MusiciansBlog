using MusiciansBlog.Application.Models;

namespace MusiciansBlog.Application.Repositories
{
    public interface ICommentsRepository
    {
        /// <summary>
        /// Создать/обновить комментарий.
        /// </summary>
        /// <param name="model">Сущность.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task CreateOrUpdateBlogAsync(Comment model, CancellationToken ct);

        /// <summary>
        /// Удалить комментарий.
        /// </summary>
        /// <param name="commentId">Id комментария.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task DeleteCommentAsync(Guid commentId, CancellationToken ct);

        /// <summary>
        /// Список.
        /// </summary>
        /// <param name="model">Фильтр (только пагинация).</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task<FilterPaginatedModel<Comment>> GetBlogCommentsAsync(BlogPaginatedModel model, CancellationToken ct);
    }
}
