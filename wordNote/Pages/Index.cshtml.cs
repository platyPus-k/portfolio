using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wordNote.Models;

namespace wordNote.Pages
{
    public class IndexModel : PageModel
    {
        public string UserName{get; private set;}

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserName = User.Identity.GetUserName();
            }
        }
    }
}
