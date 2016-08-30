namespace Bookify.App.Sdk
{
    public sealed class ApiConfig
    {
        /// <summary>
        /// Gets or sets the website base URL.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public static string Website { get; set; } = "http://localhost:8080/";

        /// <summary>
        /// Gets the books root URL.
        /// </summary>
        /// <value>
        /// The books root.
        /// </value>
        public static string BooksRoot => UrlHelper.Combine(Website, "books");

        /// <summary>
        /// Gets the files root URL.
        /// </summary>
        /// <value>
        /// The files root.
        /// </value>
        public static string FilesRoot => UrlHelper.Combine(Website, "files");

        /// <summary>
        /// Gets the genres root URL.
        /// </summary>
        /// <value>
        /// The genres root.
        /// </value>
        public static string GenresRoot => UrlHelper.Combine(Website, "genres");

        /// <summary>
        /// Gets the authentication root URL.
        /// </summary>
        /// <value>
        /// The authentication root.
        /// </value>
        public static string AuthRoot => UrlHelper.Combine(Website, "auth");

        /// <summary>
        /// Gets the person root URL.
        /// </summary>
        /// <value>
        /// The person root.
        /// </value>
        public static string PersonRoot => UrlHelper.Combine(Website, "persons");

        /// <summary>
        /// Gets the feedback root URL.
        /// </summary>
        /// <value>
        /// The feedback root.
        /// </value>
        public static string FeedbackRoot => UrlHelper.Combine(Website, "feedbacks");
        
        /// <summary>
        /// Generates a cover URL for the specified books, based on the <see cref="width"/> and <see cref="height"/> specified.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string GetCoverUrl(int bookId, int? width = null, int? height = null)
        {
            var url = UrlHelper.Combine(FilesRoot, "{bookId}/cover");
            if (width > 0 && height > 0)
            {
                url += $"?width={width}&height={height}";
            }
            url = url.Replace("{bookId}", bookId.ToString());
            return url;
        }
    }
}
