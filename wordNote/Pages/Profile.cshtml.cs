﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wordNote.Pages
{
    public class ProfileModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet() => Message = "Your contact page.";

    }
}
