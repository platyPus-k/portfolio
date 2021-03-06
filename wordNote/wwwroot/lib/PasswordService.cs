﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;


namespace wordNote.wwwroot.lib
{
    public class PasswordService : IPasswordService
    {
        // ハッシュ化...平文パスワードを渡すとハッシュ化パスワード、使用されたソルトが返る
        public (string hashedPassword, byte[] salt) HashPassword(string rawPassword)
        {
            byte[] salt = GetSalt();
            string hashed = HashPassword(rawPassword, salt);
            return (hashed, salt);
        }

        // 認証...ハッシュ化パスワード、平文パスワード・ソルトを渡すと正しいパスワードなら true が返る
        public bool VerifyPassword(string hashedPassword, string rawPassword, byte[] salt) =>
          hashedPassword == HashPassword(rawPassword, salt);

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
    }
}