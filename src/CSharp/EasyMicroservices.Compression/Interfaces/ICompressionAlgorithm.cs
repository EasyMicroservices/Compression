namespace EasyMicroservices.Compression.Interfaces
{
    public interface ICompressionAlgorithm
    {
        Span<byte> Compress(Span<byte> data);
        Span<byte> Decompress(Span<byte> compressedData);
       
    }
}
