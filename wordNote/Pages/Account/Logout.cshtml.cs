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

namespace wordNote.Pages.Account
{
    public class LogoutModel : PageModel
    {

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(Url.Content("~/"));
            }
            return Page();
        }

        // ログアウトボタンが押された起動
        public async Task<IActionResult> OnPostAsync()
        {
            // クッキーを初期化
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // ログアウト後はトップページへリダイレクト
            return LocalRedirect(Url.Content("~/"));
        }
    }
}
