using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wordNote.Models;
using Microsoft.AspNet.Identity;

namespace wordNote.Pages.Words
{
    [Authorize]
    public class MypageModel : PageModel
    {
        private readonly RazorPagesWordContext _context;
        public  string UserName { get; private set; }

        public MypageModel(RazorPagesWordContext context)
        {
            _context = context;
        }

        public IList<Word> Word { get; set; }

        public async Task OnGetAsync()
        {
            UserName = User.Identity.GetUserName();
            Word = await _context.Word.ToListAsync();
        }
    }
}