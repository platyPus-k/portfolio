using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using wordNote.Models;

namespace wordNote.Pages.Words
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RazorPagesWordContext _context;

        public CreateModel(RazorPagesWordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Word Word { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (User.Identity.IsAuthenticated)
            {
                // GetUserName() によりユーザ名を取得
                string name = User.Identity.GetUserName();
                Word.Submitter = name;
            }
            _context.Word.Add(Word);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
