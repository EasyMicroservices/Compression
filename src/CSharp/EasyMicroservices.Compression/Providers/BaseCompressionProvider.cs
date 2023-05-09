using EasyMicroservices.Compression.Interfaces;
using EasyMicroservices.Utilities.IO;
using EasyMicroservices.Utilities.IO.Interfaces;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.Providers
{
    /// <summary>
    /// Base compression provider
    /// </summary>
    public abstract class BaseCompressionProvider : ICompressionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_innerStreamMiddleware"></param>
        public BaseCompressionProvider(IStreamMiddleware _innerStreamMiddleware = default)
        {
            InnerStreamMiddleware = _innerStreamMiddleware;
        }

        /// <summary>
        /// buffer to read stream
        /// </summary>
        public static int BufferSize { get; set; } = 1024 * 1024 * 2;
        /// <summary>
        /// 
        /// </summary>
        public IStreamMiddleware InnerStreamMiddleware { get; set; }

        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <returns></returns>
        public Task<byte[]> Compress(string text)
        {
            return Compress(text, Encoding.UTF8);
        }

        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <param name="encoding">text encoding</param>
        /// <returns></returns>
        public Task<byte[]> Compress(string text, Encoding encoding)
        {
            return Compress(encoding.GetBytes(text));
        }

        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="bytes">bytes to compress</param>
        /// <returns></returns>
        public abstract Task<byte[]> Compress(byte[] bytes);
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="stream">stream to compress</param>
        /// <returns></returns>
        public async Task<byte[]> Compress(Stream stream)
        {
            return await Compress(await stream.StreamToBytesAsync(stream.Length, BufferSize));
        }
        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <returns></returns>
        public Task CompressToStream(string text, Stream streamWriter)
        {
            return CompressToStream(text, streamWriter, Encoding.UTF8);
        }
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="bytes">bytes to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <returns></returns>
        public async Task CompressToStream(byte[] bytes, Stream streamWriter)
        {
            var compressed = await Compress(bytes);
            await streamWriter.WriteAsync(compressed, 0, compressed.Length);
        }
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="streamReader">stream to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <returns></returns>
        public async Task CompressToStream(Stream streamReader, Stream streamWriter)
        {
            var readedBytes = await streamReader.StreamToBytesAsync(streamReader.Length, BufferSize);
            await CompressToStream(readedBytes, streamWriter);
        }
        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <param name="encoding">text encoding</param>
        public Task CompressToStream(string text, Stream streamWriter, Encoding encoding)
        {
            return CompressToStream(encoding.GetBytes(text), streamWriter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public abstract Task<Stream> GetStream(Stream stream);
    }
}
