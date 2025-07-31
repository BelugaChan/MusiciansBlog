using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;
using MusiciansBlog.API.Infrastructure.Comments.DeleteComment;
using MusiciansBlog.API.Infrastructure.Comments.GetComments;
using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Comments.Common
{
    public interface ICommentsRepository
    {
        /// <summary>
        /// Создать/обновить.
        /// </summary>
        /// <param name="model">Сущность.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task CreateOrUpdateCommentAsync(CommentModel model, CancellationToken ct = default);

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="commentId">Id комментария.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task DeleteCommentAsync(DeleteCommentModel model, CancellationToken ct = default);

        /// <summary>
        /// Список.
        /// </summary>
        /// <param name="model">Фильтр (только пагинация).</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task<FilterPaginatedModel<CommentModel>> GetBlogCommentsAsync(CommentsPaginatedModel model, CancellationToken ct = default);
    }
}
