using System.ComponentModel.DataAnnotations;

namespace MusiciansBlog.API.Infrastructure.Blogs.DeleteBlog
{
    public class DeleteBlogModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Required]
        public Guid BlogId { get; set; }
    }
}
