using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.Interfaces
{
    /// <summary>
    /// Compression usage interface provider
    /// </summary>
    public interface ICompressionProvider
    {
        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <returns></returns>
        Task<byte[]> Compress(string text);
        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <param name="encoding">text encoding</param>
        /// <returns></returns>
        Task<byte[]> Compress(string text, Encoding encoding);
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="bytes">bytes to compress</param>
        /// <returns></returns>
        Task<byte[]> Compress(byte[] bytes);
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="stream">stream to compress</param>
        /// <returns></returns>
        Task<byte[]> Compress(Stream stream);
        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <returns></returns>
        Task CompressToStream(string text, Stream streamWriter);
        /// <summary>
        /// compress a text
        /// </summary>
        /// <param name="text">text to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <param name="encoding">text encoding</param>
        /// <returns></returns>
        Task CompressToStream(string text, Stream streamWriter, Encoding encoding);
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="bytes">bytes to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <returns></returns>
        Task CompressToStream(byte[] bytes, Stream streamWriter);
        /// <summary>
        /// compress bytes
        /// </summary>
        /// <param name="streamReader">stream to compress</param>
        /// <param name="streamWriter">write compresstion data to this stream</param>
        /// <returns></returns>
        Task CompressToStream(Stream streamReader, Stream streamWriter);
    }
}
