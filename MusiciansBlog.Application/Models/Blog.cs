namespace MusiciansBlog.Application.Models
{
    /// <summary>
    /// Блог.
    /// </summary>
    public sealed class Blog
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid BlogId { get; set; }

        /// <summary>
        /// Id создателя блога.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Дата создания блога.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Дата обновления блога.
        /// </summary>
        public DateTimeOffset Updated { get; set; }

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
