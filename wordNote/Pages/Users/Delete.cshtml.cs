using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wordNote.Models;

namespace wordNote.Pages.Users
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesUserContext _context;

        public DeleteModel(RazorPagesUserContext context)
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

            if(UserName != user.Id)
            {
                return LocalRedirect(Url.Content("~/"));
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            user = await _context.User.FindAsync(id);

            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect(Url.Content("~/"));
        }
    }
}
