﻿using Memidło.Web.Models.Domain;
using Memidło.Web.Models.View;
using Memidło.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memidło.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IMemRepository memRepository;
              
        [BindProperty(SupportsGet = true)]
        public PaginatedPage PaginatedPage { get; set; }
        public IEnumerable<Mem> Mems { get; set; }

        public Guid UserId { get; set; }

        [BindProperty]
        public int SelectedMemId { get; set; }
        public int MemId { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMemRepository memRepository)
        {
            _logger = logger;
            this.memRepository = memRepository;
        } 

        public async Task<IActionResult> OnGet(int? pageIndex)
        {
            PaginatedPage.Count = await memRepository.CountMainAsync();

            Mems = await memRepository.GetPaginated(
               pageSize: PaginatedPage.PageSize,
               pageNumber: PaginatedPage.CurrentPage,
               isMain: true
               );

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deletedMem = await memRepository.DeleteAsync(SelectedMemId);

            if (deletedMem)
            {
                ViewData["Notification"] = new NotificationVM
                {
                    Message = "Mem został usunięty",
                    Type = Enums.NotificationType.Success
                };
            }
            else
            {
                ViewData["Notification"] = new NotificationVM
                {
                    Message = "Upss, coś poszło nie tak",
                    Type = Enums.NotificationType.Error
                };
            }

            return Page();
        }

       
    }
}