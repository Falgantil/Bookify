using System;
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
        /// <summary>
        /// The epub file as a raw byte array
        /// </summary>
        private readonly byte[] epub;

        /// <summary>
        /// The navigation points
        /// </summary>
        private List<TableOfContentNcx.NavPoint> navPoints;

        /// <summary>
        /// The table of content data.
        /// </summary>
        private TableOfContentNcx toc;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadBookViewModel" /> class.
        /// </summary>
        /// <param name="epub">The epub.</param>
        /// <param name="book">The book.</param>
        public ReadBookViewModel(byte[] epub, DetailedBookDto book)
        {
            this.Book = book;
            this.epub = epub;
        }

        /// <summary>
        /// Initializes this instance. Basically reads the EPUB byte data and parses it as an Epub.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            var memoryStream = new MemoryStream(this.epub);
            var container = await Container.Read(memoryStream);
            var package = await container.LoadPackage();
            this.toc = await package.LoadTableOfContent();
            this.navPoints = this.toc.GetOrderedNavPoints().ToList();
            this.PageCount = this.navPoints.Count;
        }

        /// <summary>
        /// Gets the total page count. You can't show pages any higher than this number.
        /// </summary>
        /// <value>
        /// The page count.
        /// </value>
        public int PageCount { get; private set; }

        /// <summary>
        /// Gets or sets the current index of the page.
        /// <para>Change this and run <see cref="LoadPage"/> to get the HTML for the current page.</para>
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <value>
        /// The book.
        /// </value>
        public DetailedBookDto Book { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance can go back.
        /// <para>Are we on the first page?</para>
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoBack => this.PageIndex > 0;

        /// <summary>
        /// Gets a value indicating whether this instance can go forward.
        /// <para>Are we on the last page?</para>
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoForward => this.PageIndex < this.PageCount - 1;

        /// <summary>
        /// Loads the HTML from the current page. Set <see cref="PageIndex" /> to change what page to load
        /// </summary>
        /// <returns>The HTML for the current page.</returns>
        /// <exception cref="ArgumentException">Invalid page index!</exception>
        public async Task<string> LoadPage()
        {
            if (this.PageIndex < 0 || this.PageIndex >= this.PageCount)
            {
                throw new ArgumentException("Invalid page index!", nameof(this.PageIndex));
            }
            var navPoint = this.navPoints[this.PageIndex];
            var htmlFile = await this.toc.GetHtmlFile(navPoint);
            var page = await htmlFile.ReadContent();
            return page.ToString();
        }
    }
}