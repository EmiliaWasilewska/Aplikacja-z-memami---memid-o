using System.ComponentModel.DataAnnotations;

namespace Memidło.Web.Models.View
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
