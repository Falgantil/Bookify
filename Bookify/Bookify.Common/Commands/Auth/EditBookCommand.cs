using System.Collections.Generic;

namespace Bookify.Common.Commands.Auth
{
    public class EditBookCommand
    {
        /// <summary>
        /// The isbn
        /// </summary>
        public string ISBN;
        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        public IEnumerable<int> Genres { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary { get; set; }
        /// <summary>
        /// Gets or sets the author identifier.
        /// </summary>
        /// <value>
        /// The author identifier.
        /// </value>
        public int? AuthorId { get; set; }
        /// <summary>
        /// Gets or sets the publish year.
        /// </summary>
        /// <value>
        /// The publish year.
        /// </value>
        public int? PublishYear { get; set; }
        /// <summary>
        /// Gets or sets the publisher identifier.
        /// </summary>
        /// <value>
        /// The publisher identifier.
        /// </value>
        public int? PublisherId { get; set; }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the copies available.
        /// </summary>
        /// <value>
        /// The copies available.
        /// </value>
        public int? CopiesAvailable { get; set; }
        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        /// <value>
        /// The page count.
        /// </value>
        public int? PageCount { get; set; }
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal? Price { get; set; }
        /// <summary>
        /// Gets or sets the view count.
        /// </summary>
        /// <value>
        /// The view count.
        /// </value>
        public int? ViewCount { get; set; }
    }
}