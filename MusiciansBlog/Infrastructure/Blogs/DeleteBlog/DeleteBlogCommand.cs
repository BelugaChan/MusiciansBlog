using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MusiciansBlog.API.Infrastructure.Blogs.DeleteBlog
{
    public class DeleteBlogCommand : IRequest
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Required]
        public Guid BlogId { get; set; }
    }
}
