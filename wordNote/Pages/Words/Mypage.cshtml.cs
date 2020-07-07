﻿using System;
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
    public class MypageModel : PageModel
    {
        private readonly RazorPagesWordContext _context;

        public MypageModel(RazorPagesWordContext context)
        {
            _context = context;
        }

        public IList<Word> Word { get; set; }

        public async Task OnGetAsync()
        {
            Word = await _context.Word.ToListAsync();
        }
    }
}