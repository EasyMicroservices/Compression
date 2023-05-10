using EasyMicroservices.Compression.IO;
using EasyMicroservices.Utilities.IO.Interfaces;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.Providers
{
    /// <summary>
    /// Decompress with GZip algorithm
    /// </summary>
    public class GZipDecompressionProvider : BaseDecompressionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_innerStreamMiddleware"></param>
        public GZipDecompressionProvider(IStreamMiddleware _innerStreamMiddleware = default) : base(_innerStreamMiddleware)
        {
        }
        /// <summary>
        /// decompress bytes
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        public override async Task<byte[]> Decompress(byte[] bytes)
        {
            using var compressedStream = new MemoryStream(bytes);
            using var stream = new MemoryStream();
            using var newStream = await GetStream(compressedStream);
            await newStream.CopyToAsync(stream);
            return stream.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public override Task<Stream> GetStream(Stream stream)
        {
            return InnerGZipStream.GetStream(stream, InnerStreamMiddleware);
        }
    }
}
