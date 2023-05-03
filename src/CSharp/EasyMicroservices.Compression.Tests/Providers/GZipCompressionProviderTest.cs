using EasyMicroservices.Compression.Providers;

namespace EasyMicroservices.Compression.Tests.Providers
{
    public class GZipCompressionProviderTest : BaseCompressionProviderTest
    {
        public GZipCompressionProviderTest() : base(new GZipCompressionProvider(), new GZipDecompressionProvider())
        {
        }
    }
}
