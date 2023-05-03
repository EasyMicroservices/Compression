using System;
using System.IO.Compression;
using System.IO;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.Providers
{
    /// <summary>
    /// Decompress with GZip algorithm
    /// </summary>
    public class GZipDecompressionProvider : BaseDecompressionProvider
    {
        /// <summary>
        /// decompress bytes
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        public override async Task<byte[]> Decompress(byte[] bytes)
        {
            using var stream = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            using var gzipStream = new GZipStream(stream, CompressionMode.Decompress);
            await gzipStream.WriteAsync(bytes, 0, bytes.Length);
            await gzipStream.FlushAsync();
            await stream.FlushAsync();
            return stream.ToArray();
        }
    }
}
