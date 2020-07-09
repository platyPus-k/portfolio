using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wordNote.Models;

namespace wordNote.Pages.Words
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly RazorPagesWordContext _context;

        public EditModel(RazorPagesWordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Word Word { get; set; }

        public string UserName { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserName = User.Identity.GetUserName();
            if (id == null)
            {
                return LocalRedirect(Url.Content("~/Words/Mypage"));
            }

            Word = await _context.Word.FirstOrDefaultAsync(m => m.Id == id);

            if (Word == null)
            {
                return LocalRedirect(Url.Content("~/Words/Mypage"));
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string name = User.Identity.GetUserName();
            if (Word.Submitter != name) return Page();
            _context.Attach(Word).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordExists(Word.Id))
                {
                    return Page();
                }
                else
                {
                    throw;
                }
            }

            return LocalRedirect(Url.Content("~/Words/Mypage"));
        }

        private bool WordExists(int id)
        {
            return _context.Word.Any(e => e.Id == id);
        }
    }
}
