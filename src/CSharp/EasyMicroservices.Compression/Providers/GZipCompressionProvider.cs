using EasyMicroservices.Compression.IO;
using EasyMicroservices.Utilities.IO.Interfaces;
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
        /// 
        /// </summary>
        /// <param name="_innerStreamMiddleware"></param>
        public GZipCompressionProvider(IStreamMiddleware _innerStreamMiddleware = default) : base(_innerStreamMiddleware)
        {
        }
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="bytes">bytes to compress</param>
        /// <returns></returns>
        public override async Task<byte[]> Compress(byte[] bytes)
        {
            using var stream = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            using var newStream = await GetStream(stream);
            await newStream.WriteAsync(bytes, 0, bytes.Length);
            newStream.Close();
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
