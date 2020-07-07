using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Word = await _context.Word.FirstOrDefaultAsync(m => m.Id == id);

            if (Word == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Word = await _context.Word.FindAsync(id);

            if (Word != null)
            {
                _context.Word.Remove(Word);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
