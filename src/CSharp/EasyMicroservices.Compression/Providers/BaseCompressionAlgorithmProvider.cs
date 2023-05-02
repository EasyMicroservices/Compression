using EasyMicroservices.Compression.Interfaces;
using System.IO.Compression;

namespace EasyMicroservices.Compression.Providers
{
    public class BaseCompressionAlgorithmProvider : ICompressionAlgorithm
    {
        public Span<byte> Compress(Span<byte> data)
        {
            using (var stream = new MemoryStream())
            {
                using (var compressor = new GZipStream(stream, CompressionMode.Compress))
                {
                    compressor.Write(data);
                }
                return stream.ToArray();
            }
        }      

        public Span<byte> Decompress(Span<byte> compressedData)
        {
            using (var stream = new MemoryStream(compressedData.ToArray()))
            using (var decompressor = new GZipStream(stream, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                decompressor.CopyTo(output);
                return output.ToArray();
            }
        }       
    }
}
