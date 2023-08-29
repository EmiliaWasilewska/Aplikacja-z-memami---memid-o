using Memidło.Web.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Memidło.Web.Models.View
{
    public class MemVM
    {
        public int Id { get; set; }

        [Required]
        public string PageTitle { get; set; }


        public string Heading { get; set; }


        public string Content { get; set; }

        [Required]
        public string FeaturedImageUrl { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public string Author { get; set; }

        public bool Main { get; set; }

    }
}
