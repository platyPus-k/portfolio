using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wordNote.Models;

namespace wordNote.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesUserContext _context;

        public EditModel(RazorPagesUserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User user { get; set; }

        public string UserName { get; private set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserName = User.Identity.GetUserName();

            if (id == null)
            {
                return LocalRedirect(Url.Content("~/"));
            }

            user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return LocalRedirect(Url.Content("~/"));
            }

            if (UserName != user.Id)
            {
                return LocalRedirect(Url.Content("~/"));
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

            _context.Attach(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
