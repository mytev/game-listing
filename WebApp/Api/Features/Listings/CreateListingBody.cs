using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    public class CreateListingBody
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(100)]
        public string SubTitle { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}