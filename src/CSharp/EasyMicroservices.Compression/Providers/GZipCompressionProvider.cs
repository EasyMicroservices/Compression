using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.Providers
{
    /// <summary>
    /// gzip compression
    /// </summary>
    public class GZipCompressionProvider : BaseCompressionProvider
    {
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="bytes">bytes to compress</param>
        /// <returns></returns>
        public override async Task<byte[]> Compress(byte[] bytes)
        {
            using var stream = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            using var gzipStream = new GZipStream(stream, CompressionMode.Compress);
            await gzipStream.WriteAsync(bytes, 0, bytes.Length);
            gzipStream.Close();
            return stream.ToArray();
        }
    }
}
