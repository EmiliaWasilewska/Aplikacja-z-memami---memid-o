using Memidło.Web.Models.Domain;
using Memidło.Web.Models.View;
using Memidło.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memidło.Web.Pages.Mems
{
    public class RandomMemModel : PageModel
    {
        private readonly IMemRepository memRepository;

        [BindProperty]
        public Mem? Mem { get; set; }
        public RandomMemModel(IMemRepository memRepository)
        {
            this.memRepository = memRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            Mem = await memRepository.GetRandom();

            return Page();
        }
    }
}
