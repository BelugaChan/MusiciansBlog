using MusiciansBlog.API.Infrastructure.Blogs.DeleteBlog;
using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;
using MusiciansBlog.API.Infrastructure.Common;

namespace MusiciansBlog.API.Infrastructure.Blogs.Common
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    public interface IBlogsRepository
    {
        /// <summary>
        /// Создать/обновить.
        /// </summary>
        /// <param name="model">Сущность.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task CreateOrUpdateBlogAsync(BlogModel model, CancellationToken ct = default);

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="blogId">Id блога.</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task DeleteBlogAsync(DeleteBlogModel model, CancellationToken ct = default);

        /// <summary>
        /// Список.
        /// </summary>
        /// <param name="model">Фильтр (только пагинация).</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns></returns>
        Task<FilterPaginatedModel<BlogModel>> GetBlogsAsync(BlogsPaginatedModel model, CancellationToken ct = default);
    }
}
