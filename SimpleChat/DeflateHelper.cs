using System.IO;
using System.IO.Compression;

namespace SimpleChat
{
    public static class DeflateHelper
    {
        public static byte[] Compress(byte[] bytes)
        {
            using (var buffer = new MemoryStream())
            {
                using (var ms = new MemoryStream(bytes))
                using (var cs = new DeflateStream(buffer, CompressionMode.Compress))
                    ms.CopyTo(cs);
                return buffer.ToArray();
            }
        }

        public static byte[] Decompress(byte[] bytes)
        {
            using (var buffer = new MemoryStream())
            {
                using (var ms = new MemoryStream(bytes))
                using (var ds = new DeflateStream(ms, CompressionMode.Decompress))
                    ds.CopyTo(buffer);
                return buffer.ToArray();
            }
        }
    }
}
