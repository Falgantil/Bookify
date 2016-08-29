using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bookify.Common.Models;
using EpubReader.Net.Core;

namespace Bookify.App.Core.ViewModels
{
    public class ReadBookViewModel : BaseViewModel
    {
        private readonly byte[] epub;
        private List<TableOfContentNcx.NavPoint> navPoints;
        private TableOfContentNcx toc;

        public ReadBookViewModel(byte[] epub, DetailedBookDto book)
        {
            this.Book = book;
            this.epub = epub;
        }

        public async Task Initialize()
        {
            var memoryStream = new MemoryStream(this.epub);
            var container = await Container.Read(memoryStream);
            var package = await container.LoadPackage();
            this.toc = await package.LoadTableOfContent();
            this.navPoints = this.toc.GetOrderedNavPoints().ToList();
            this.PageCount = this.navPoints.Count;
        }

        public int PageCount { get; private set; }

        public int PageIndex { get; set; }

        public DetailedBookDto Book { get; private set; }

        public bool CanGoBack => this.PageIndex > 0;
        public bool CanGoForward => this.PageIndex < this.PageCount - 1;

        public async Task<string> LoadPage()
        {
            var navPoint = this.navPoints[this.PageIndex];
            var htmlFile = await this.toc.GetHtmlFile(navPoint);
            var page = await htmlFile.ReadContent();
            return page.ToString();
        }
    }
}