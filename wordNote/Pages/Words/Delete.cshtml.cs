using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wordNote.Models;

namespace wordNote.Pages.Words
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWordContext _context;

        public DeleteModel(RazorPagesWordContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            string name = User.Identity.GetUserName();
            if (Word.Submitter != name) return Page();

            if (id == null)
            {
                return Page();
            }

            Word = await _context.Word.FindAsync(id);

            if (Word != null)
            {
                _context.Word.Remove(Word);
                await _context.SaveChangesAsync();
            }

            return LocalRedirect(Url.Content("~/Words/Mypage"));
        }
    }
}
