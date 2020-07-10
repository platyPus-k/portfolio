using System;

namespace wordNote.wwwroot.lib
{
    public class IPasswordService
    {
        public interface IPasswordService
        {
            bool VerifyPassword(string hashedPassword, string rawPassword, byte[] salt);
            (string hashedPassword, byte[] salt) HashPassword(string rawPassword);
        }
    }
}
