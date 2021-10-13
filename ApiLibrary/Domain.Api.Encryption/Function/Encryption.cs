using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Api.Encryption
{
    public class Encryption : IEncryption
    {
        private readonly IEncryption _encryption;

        public Encryption()
        {
            _encryption = new Argon2();
        }

        public EncryptionResult Create(string password)
        {
            return _encryption.Create(password);
        }
        public string Encrypt(string password, string salt)
        {
            return _encryption.Encrypt(password, salt);
        }
        public bool Validate(string password, string newPassword, string salt)
        {
            return _encryption.Validate(password, newPassword, salt);
        }
    }
}
