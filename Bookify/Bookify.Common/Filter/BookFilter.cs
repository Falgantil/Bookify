namespace Bookify.Common.Filter
{
    /// <summary>
    /// Book filer, filters the books by the available params.
    /// </summary>
    /// <seealso cref="Bookify.Common.Filter.BaseFilter" />
    public class BookFilter : BaseFilter
    {
        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        public int[] Genres { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public int? Author { get; set; }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public string OrderBy { get; set; } = "Price";

        /// <summary>
        /// Gets or sets a value indicating is descending or ascending.
        /// </summary>
        /// <value>
        ///   <c>true</c> if descending; otherwise, <c>false</c>.
        /// </value>
        public bool Descending { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [my books].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [my books]; otherwise, <c>false</c>.
        /// </value>
        public bool MyBooks { get; set; }
    }
}