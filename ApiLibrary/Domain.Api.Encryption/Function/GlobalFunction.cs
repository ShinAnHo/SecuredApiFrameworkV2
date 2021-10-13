using System.Security.Cryptography;

namespace Domain.Api.Encryption
{
    internal class GlobalFunction
    {
        public static byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
    }
}
