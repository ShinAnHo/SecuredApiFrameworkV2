using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Api.Encryption
{
    public class EncryptionResult
    {
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}
