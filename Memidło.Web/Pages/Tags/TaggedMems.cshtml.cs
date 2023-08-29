using Memidło.Web.Models.Domain;
using Memidło.Web.Models.View;
using Memidło.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace Memidło.Web.Pages.Tags
{
    public class TaggedMemsModel : PageModel
    {
        private readonly IMemRepository memRepository;

        [BindProperty]
        public List<Mem> Mems { get; set; }

        [BindProperty]
        public string TagName { get; set; }

        [BindProperty(SupportsGet = true)]
        public PaginatedPage PaginatedPage { get; set; }

        public TaggedMemsModel(IMemRepository memRepository)
        {
            this.memRepository = memRepository;
        }

        public async Task<IActionResult> OnGet(string tagName)
        {
            PaginatedPage.Count = await memRepository.CountTaggedAsync(tagName);

            TagName = tagName;

            Mems = (await memRepository.GetAllAsync(tagName))
                  .OrderByDescending(x => x.PublishDate)
                  .Skip((PaginatedPage.CurrentPage - 1) * PaginatedPage.PageSize).Take(PaginatedPage.PageSize)
                  .ToList();
            
            return Page();
        }
    }
}
