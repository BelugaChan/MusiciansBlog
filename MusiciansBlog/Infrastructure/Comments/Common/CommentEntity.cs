using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Users.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansBlog.API.Infrastructure.Comments.Common
{
    /// <summary>
    /// Комментарий
    /// </summary>
    [Table("comments", Schema = "MusiciansBlog")]
    public sealed class CommentEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        [Column("id")]
        public Guid CommentId { get; set; }

        /// <summary>
        /// Id создателя комментария.
        /// </summary>
        [ForeignKey(nameof(Author))]
        [Column("authorId")]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Id родительского блога.
        /// </summary>
        [ForeignKey(nameof(ParentBlog))]
        [Column("parentBlogId")]
        public Guid ParentBlogId { get; set; }

        /// <summary>
        /// Дата создания комментария.
        /// </summary>
        [Column("created")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Дата обновления комментария.
        /// </summary>
        [Column("updated")]
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Количество лайков.
        /// </summary>
        [Column("likeCount")]
        public int LikeCount { get; set; }

        /// <summary>
        /// Количество дизлайков.
        /// </summary>
        [Column("dislikeCount")]
        public int DislikeCount { get; set; }

        /// <summary>
        /// Флаг удаления комментария.
        /// </summary>
        [Column("isDeleted")]
        public bool IsDeleted { get; set; }

        public UserEntity Author { get; set; } = null!;

        public BlogEntity ParentBlog { get; set; } = null!;
    }
}
