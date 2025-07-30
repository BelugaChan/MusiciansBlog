using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansBlog.Logic.Entities
{
    /// <summary>
    /// Блог
    /// </summary>
    [Index(nameof(Title))]
    [Table("Blogs", Schema = "MusiciansBlog")]
    public sealed class BlogEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        [Column("id")]
        public Guid BlogId { get; set; }

        /// <summary>
        /// Id создателя блога.
        /// </summary>
        [ForeignKey(nameof(Author))]
        [Column("authorId")]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Дата создания блога.
        /// </summary>
        [Column("created")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Дата обновления блога.
        /// </summary>
        [Column("updated")]
        public DateTimeOffset Updated { get; set; }

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
        /// Заголовок блога.
        /// </summary>
        [Column("title", TypeName = "varchar(200)")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Контент блога.
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Флаг удаления блога.
        /// </summary>
        [Column("isDeleted")]
        public bool IsDeleted { get; set; }

        public UserEntity Author { get; set; } = null!;

        public List<CommentEntity> Comments { get; set; } = new();
    }
}
