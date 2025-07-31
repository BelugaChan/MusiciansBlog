namespace MusiciansBlog.API.Infrastructure.Blogs.Common
{
    /// <summary>
    /// Блог.
    /// </summary>
    public sealed class BlogModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid BlogId { get; set; } = Guid.Empty;

        /// <summary>
        /// Id создателя блога.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Дата создания блога.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Количество лайков.
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// Количество дизлайков.
        /// </summary>
        public int DislikeCount { get; set; }

        /// <summary>
        /// Заголовок блога.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Контент блога.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
