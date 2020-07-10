using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using wordNote.Models;

namespace wordNote.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesUserContext _context;

        public CreateModel(RazorPagesUserContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(Url.Content("~/"));
            }

            return Page();
        }

        [BindProperty]
        public User user { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            (string hashed, byte[] salt) = HashPassword(user.Password);

            var masterUser = new User
            {
                Id = user.Id,
                Password = hashed,
                Name = user.Name,
                Age = user.Age,
                Salt = salt
            };

            bool userCheck = VerifyPassword(hashed, user.Password, salt);
            if (!userCheck) return Page();

            _context.User.Add(masterUser);
            await _context.SaveChangesAsync();

            return LocalRedirect(Url.Content("~/Account/Login"));
        }

        public (string hashedPassword, byte[] salt) HashPassword(string rawPassword)
        {
            byte[] salt = GetSalt();
            string hashed = HashPassword(rawPassword, salt);
            return (hashed, salt);
        }

        private string HashPassword(string rawPassword, byte[] salt) =>
          Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
          password: rawPassword,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA512,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

        private byte[] GetSalt()
        {
            using (var gen = RandomNumberGenerator.Create())
            {
                var salt = new byte[128 / 8];
                gen.GetBytes(salt);
                return salt;
            }
        }

        public bool VerifyPassword(string hashedPassword, string rawPassword, byte[] salt) =>
        hashedPassword == HashPassword(rawPassword, salt);
    }
}
