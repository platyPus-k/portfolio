using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using wordNote.Models;

namespace wordNote.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesUserContext _context;

        public CreateModel(RazorPagesUserContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(Url.Content("~/"));
            }
              
            return Page();
        }

        [BindProperty]
        public User user { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return LocalRedirect(Url.Content("~/Account/Login"));
        }
    }
}
