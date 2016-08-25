using System;
using System.Linq;

using Shouldly;
using VersFx.Formats.Text.Epub;
using Xunit;
using Xunit.Abstractions;

namespace Bookify.App.Core.Tests.Services
{
    public class EpubReaderTests
    {
        private readonly ITestOutputHelper output;

        public EpubReaderTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("book1")]
        [InlineData("book2")]
        public void VerifyEpubReaderCanOpenEpubFiles(string fileName)
        {
            var book = EpubReader.OpenBook($"Files/{fileName}.epub");
            book.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("book1")]
        [InlineData("book2")]
        public void VerifyEpubReaderCanReadEpubFiles(string fileName)
        {
            var book = new Epub($"Files/{fileName}.epub");
            book.Title.Count.ShouldBeGreaterThan(0);
            this.output.WriteLine($"Title was {string.Join(" - ", book.Title)}");
            var html = book.GetContentAsHtml();
            html.ShouldNotBeNullOrEmpty();
            this.output.WriteLine($"Html: {new string(html.Take(Math.Min(500, html.Length)).ToArray())}");
        }

        [Theory]
        [InlineData("book1")]
        [InlineData("book2")]
        public void VerifyEpubReaderCanReadChapters(string fileName)
        {
            var book = new Epub($"Files/{fileName}.epub");
            var chapters = book.Content;
            chapters.Count.ShouldBeGreaterThan(0);
        }
    }
}
