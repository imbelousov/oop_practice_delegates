using System;
using System.IO;
using System.Security.Cryptography;

namespace SimpleChat
{
    public static class AesHelper
    {
        private static readonly byte[] Key = Convert.FromBase64String("K163fm4T+H9PhIok+N/aI49kvnXWVu4DAcbKsWGlJw0=");
        private static readonly byte[] IV = Convert.FromBase64String("BE5w42IgGhuI8U2YWguqEQ==");

        public static byte[] Encrypt(byte[] bytes)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        cs.Write(bytes);
                    return ms.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] bytes)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream(bytes))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var buffer = new MemoryStream(bytes))
                {
                    cs.CopyTo(buffer);
                    return buffer.ToArray();
                }
            }
        }
    }
}
