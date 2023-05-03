using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.Interfaces
{
    /// <summary>
    /// Decompression usage interface provider
    /// </summary>
    public interface IDecompressionProvider
    {
        /// <summary>
        /// Decompress to text
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        Task<string> DecompressToText(byte[] bytes);
        /// <summary>
        /// Decompress to text
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <param name="encoding">text encoding</param>
        /// <returns></returns>
        Task<string> DecompressToText(byte[] bytes, Encoding encoding);
        /// <summary>
        /// decompress bytes
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        Task<byte[]> Decompress(byte[] bytes);
        /// <summary>
        /// decompress stream
        /// </summary>
        /// <param name="stream">decompress stream</param>
        /// <returns></returns>
        Task<byte[]> Decompress(Stream stream);
        /// <summary>
        /// Decompress to stream
        /// </summary>
        /// <param name="bytes">bytes to decompress</param>
        /// <returns></returns>
        Task<Stream> DecompressToStream(byte[] bytes);
    }
}
