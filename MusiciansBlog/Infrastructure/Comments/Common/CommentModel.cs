namespace MusiciansBlog.API.Infrastructure.Comments.Common
{
    /// <summary>
    /// Комментарий.
    /// </summary>
    public sealed class CommentModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid CommentId { get; set; } = Guid.Empty;

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
