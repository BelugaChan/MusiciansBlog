namespace MusiciansBlog.Application.Models
{
    /// <summary>
    /// Комментарий.
    /// </summary>
    public sealed class Comment
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid CommentId { get; set; }

        /// <summary>
        /// Id создателя комментария.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Id родительского блога.
        /// </summary>
        public Guid ParentBlogId { get; set; }

        /// <summary>
        /// Дата создания комментария.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Дата обновления комментария.
        /// </summary>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Количество лайков.
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// Количество дизлайков.
        /// </summary>
        public int DislikeCount { get; set; }
    }
}
