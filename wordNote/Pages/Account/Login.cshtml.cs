using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using wordNote.Models;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace wordNote.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        // コンストラクタでテーブルのコンテキストを受け取る
        public LoginModel(RazorPagesUserContext context)
        {
            _context = context;
        }

        // htmlタグから詠み込み(asp-for="xxx")
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            // プロパティの属性の定義
            // 入力されたIdの定義
            [Required]
            [DataType(DataType.Text)]
            public string Id { get; set; }

            // 入力されたPasswordの定義
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            // セッションへの記憶ボタンの定義
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        // エラーメッセージプロパティ
        [TempData]
        public string ErrorMessage { get; set; }

        // ユーザーテーブルコンテキストの定義(Userテーブルのオブジェクト)
        // 受け取ったコンテキストを読み取る
        private readonly RazorPagesUserContext _context;

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(Url.Content("~/"));
            }
            return Page(); 
        }

        // ログインボタンがクリックされてpostリクエストを受け付けたときに動く
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // 入力がない場合はログイン出来ない
            if (!ModelState.IsValid)return Page();

            // ==================================================
            // テストの認証
            //var testDB = new[] {
            //(userid: "bbb", password: "word1"),
            //(userid: "ccc", password: "word2")
            //};
            //bool isValid = testDB.Any(
            //x => x.userid == Input.Userid &&
            // x.password == Input.Password);

            // 入力されたIdをFindAsyncメソッドで参照してインスタンスを定義
            // Inputとインスタンスのユーザー情報を比較し一致しなければページリターン
            var user = await _context.User.FindAsync(Input.Id);
            var salt = user.Salt;
            bool checkPass = VerifyPassword(user.Password,Input.Password,salt); 
            
            if (user is null)
            {
                return Page();
            }
            if (!checkPass)
            {
                return Page();
            }

            // 以下ログイン処理 
            Claim[] claims = {
            new Claim(ClaimTypes.NameIdentifier, Input.Id), // ユニークID
            new Claim(ClaimTypes.Name, Input.Id),
            };

            // 一意の ID 情報
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // ログイン
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    // Cookieをブラウザーセッション間で永続化するかどうか？(ブラウザを閉じてもログアウトしないかどうか)
                    IsPersistent = Input.RememberMe
                });

            // ログイン成功後スタートページへ遷移
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }

        public bool VerifyPassword(string hashedPassword, string rawPassword, byte[] salt)
        {
            if(hashedPassword == HashPassword(rawPassword, salt))
            {
                return true;
            }
            return false;
        }

        private string HashPassword(string rawPassword, byte[] salt) =>
        Convert.ToBase64String(
        KeyDerivation.Pbkdf2(
          password: rawPassword,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA512,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

    }
}