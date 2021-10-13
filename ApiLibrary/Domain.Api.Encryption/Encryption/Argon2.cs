using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Api.Encryption
{
    internal class Argon2 : IEncryption
    {
        public EncryptionResult Create(string password)
        {
            var salt = GlobalFunction.CreateSalt();
            var hash = HashPassword(password, salt);

            var encryptionResult = new EncryptionResult()
            {
                Salt = Convert.ToBase64String(salt),
                Hash = Convert.ToBase64String(hash)
            };

            return encryptionResult;
        }
        public string Encrypt(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var newHash = HashPassword(password, saltBytes);

            return Convert.ToBase64String(newHash);
        }
        public bool Validate(string password, string newPassword, string salt)
        {
            var passwordBytes = Convert.FromBase64String(password);
            var saltBytes = Convert.FromBase64String(salt);
            var newHash = HashPassword(newPassword, saltBytes);

            return newHash.SequenceEqual(passwordBytes);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8, // 4 Cores
                Iterations = 2,
                MemorySize = 1024 * 1024 // 1 GB
            };

            return argon2.GetBytes(16);
        }
    }
}
