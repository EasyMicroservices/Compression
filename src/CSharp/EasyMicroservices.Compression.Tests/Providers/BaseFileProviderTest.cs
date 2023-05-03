using EasyMicroservices.Compression.Interfaces;
using EasyMicroservices.Utilities.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.Compression.Tests.Providers
{
    public abstract class BaseCompressionProviderTest
    {
        protected readonly ICompressionProvider _compressionProvider;
        protected readonly IDecompressionProvider _decompressionProvider;
        public BaseCompressionProviderTest(ICompressionProvider compressionProvider, IDecompressionProvider decompressionProvider)
        {
            _compressionProvider = compressionProvider;
            _decompressionProvider = decompressionProvider;
        }

        protected string GetRandomString()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                stringBuilder.Append(chars.OrderByDescending(x => Guid.NewGuid()).ToString());
            }
            return stringBuilder.ToString();
        }

        protected byte[] GetRandomBytes()
        {
            return Encoding.UTF8.GetBytes(GetRandomString());
        }

        protected Stream GetRandomStream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(GetRandomString()));
        }

        public virtual Task OnInitialize()
        {
            return TaskHelper.GetCompletedTask();
        }

        [Fact]
        public virtual async Task TestByteCompress()
        {
            await OnInitialize();
            var bytes = GetRandomBytes();
            var compressedBytes = await _compressionProvider.Compress(bytes);
            Assert.NotEqual(compressedBytes, bytes);
            var decompressedBytes = await _decompressionProvider.Decompress(compressedBytes);
            Assert.Equal(decompressedBytes, bytes);
        }

        [Fact]
        public virtual async Task TestTextCompress()
        {
            await OnInitialize();
            var text = GetRandomString();
            var compressedBytes = await _compressionProvider.Compress(text);
            var decompressedText = await _decompressionProvider.DecompressToText(compressedBytes);
            Assert.Equal(text, decompressedText);
        }

        [Fact]
        public virtual async Task TestTextEncodingCompress()
        {
            await OnInitialize();
            var text = GetRandomString();
            var compressedBytes = await _compressionProvider.Compress(text, Encoding.ASCII);
            var decompressedText = await _decompressionProvider.DecompressToText(compressedBytes, Encoding.ASCII);
            Assert.Equal(text, decompressedText);
        }

        [Fact]
        public virtual async Task TestStreamCompress()
        {
            await OnInitialize();
            var stream = GetRandomStream();
            var compressedBytes = await _compressionProvider.Compress(stream);
            var decompressedStream = await _decompressionProvider.DecompressToStream(compressedBytes);
            var decompressedBytes = await decompressedStream.StreamToBytesAsync(decompressedStream.Length, 1024);
            stream.Seek(0, SeekOrigin.Begin);
            var mainbytes = await stream.StreamToBytesAsync(stream.Length, 1024);
            Assert.Equal(decompressedBytes, mainbytes);
        }

        [Fact]
        public virtual async Task TestBytesAsStreamCompress()
        {
            await OnInitialize();
            var bytes = GetRandomBytes();
            using MemoryStream memoryStream = new MemoryStream();
            await _compressionProvider.CompressToStream(bytes, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var decompressedBytes = await _decompressionProvider.Decompress(memoryStream);
            Assert.Equal(decompressedBytes, bytes);
        }

        [Fact]
        public virtual async Task TestTextAsStreamCompress()
        {
            await OnInitialize();
            var text = GetRandomString();
            using MemoryStream memoryStream = new MemoryStream();
            await _compressionProvider.CompressToStream(text, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var decompressedText = await _decompressionProvider.DecompressToText(memoryStream.ToArray());
            Assert.Equal(text, decompressedText);
        }

        [Fact]
        public virtual async Task TestTextEncodingAsStreamCompress()
        {
            await OnInitialize();
            var text = GetRandomString();
            using MemoryStream memoryStream = new MemoryStream();
            await _compressionProvider.CompressToStream(text, memoryStream, Encoding.ASCII);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var decompressedText = await _decompressionProvider.DecompressToText(memoryStream.ToArray(), Encoding.ASCII);
            Assert.Equal(text, decompressedText);
        }

        [Fact]
        public virtual async Task TestStreamAsStreamCompress()
        {
            await OnInitialize();
            var stream = GetRandomStream();
            using MemoryStream memoryStream = new MemoryStream();
            await _compressionProvider.CompressToStream(stream, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var decompressedBytes = await _decompressionProvider.Decompress(memoryStream.ToArray());
            stream.Seek(0, SeekOrigin.Begin);
            var mainbytes = await stream.StreamToBytesAsync(stream.Length, 1024);
            Assert.Equal(decompressedBytes, mainbytes);
        }
    }
}
