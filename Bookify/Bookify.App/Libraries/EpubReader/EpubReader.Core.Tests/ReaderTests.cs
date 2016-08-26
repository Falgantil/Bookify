using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace EpubReader.Core.Tests
{
    public class ReaderTests
    {
        [Fact]
        public async Task TestClass()
        {
            var stream = File.ReadAllBytes("Files/book1.epub");
            var reader = new Reader();
            await reader.Read(new MemoryStream(stream));
        }
    }
}