using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MusiciansBlog.API.Infrastructure.Blogs.AddOrUpdateBlog
{
    public class AddOrUpdateBlogCommand : IRequest
    {
        /// <summary>
        /// Id создателя блога.
        /// </summary>
        [Required]
        public Guid AuthorId { get; set; }

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

        /// <summary>
        /// Заголовок блога.
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Контент блога.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
