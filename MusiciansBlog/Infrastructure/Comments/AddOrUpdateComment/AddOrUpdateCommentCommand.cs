using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MusiciansBlog.API.Infrastructure.Comments.AddOrUpdateComment
{
    public class AddOrUpdateCommentCommand : IRequest
    {
        /// <summary>
        /// Id создателя комментария.
        /// </summary>
        [Required]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Id родительского блога.
        /// </summary>
        [Required]
        public Guid ParentBlogId { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        [Required]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Количество лайков.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int LikeCount { get; set; }

        /// <summary>
        /// Количество дизлайков.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int DislikeCount { get; set; }
    }
}
