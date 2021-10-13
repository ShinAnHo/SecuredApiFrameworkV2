using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Api.Encryption
{
    public interface IEncryption
    {
        EncryptionResult Create(string password);
        string Encrypt(string password, string salt);
        bool Validate(string password, string newPassword, string salt);
    }
}
