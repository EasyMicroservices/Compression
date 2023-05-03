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
            using var compressedStream = new MemoryStream(bytes);
            using var stream = new MemoryStream();
            using var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            await gzipStream.CopyToAsync(stream);
            return stream.ToArray();
        }
    }
}
