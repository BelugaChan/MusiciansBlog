using System.ComponentModel.DataAnnotations;

namespace MusiciansBlog.Application.Models
{
    public class PaginatedModel
    {
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }
    }
}
