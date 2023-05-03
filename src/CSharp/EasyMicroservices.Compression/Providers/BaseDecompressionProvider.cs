using EasyMicroservices.Compression.Interfaces;
using EasyMicroservices.Utilities.IO;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.Providers
{
    /// <summary>
    /// base of decompression
    /// </summary>
    public abstract class BaseDecompressionProvider : IDecompressionProvider
    {
        /// <summary>
        /// buffer to read stream
        /// </summary>
        public static int BufferSize { get; set; } = 1024 * 1024 * 2;

        /// <summary>
        /// Decompress to text
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        public Task<string> DecompressToText(byte[] bytes)
        {
            return DecompressToText(bytes, Encoding.UTF8);
        }
        /// <summary>
        /// Decompress to text
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <param name="encoding">text encoding</param>
        /// <returns></returns>
        public async Task<string> DecompressToText(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(await Decompress(bytes));
        }
        /// <summary>
        /// decompress bytes
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        public abstract Task<byte[]> Decompress(byte[] bytes);
        /// <summary>
        /// decompress stream
        /// </summary>
        /// <param name="stream">decompress stream</param>
        /// <returns></returns>
        public async Task<byte[]> Decompress(Stream stream)
        {
            return await Decompress(await stream.StreamToBytesAsync(stream.Length, BufferSize));
        }

        /// <summary>
        /// Decompress to stream
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        public async Task<Stream> DecompressToStream(byte[] bytes)
        {
            return new MemoryStream(await Decompress(bytes));
        }
    }
}
