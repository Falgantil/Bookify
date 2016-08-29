namespace Bookify.App.Sdk
{
    public sealed class ApiConfig
    {
        public static string Website { get; set; } = "http://localhost:8080/";

        public static string BooksRoot => UrlHelper.Combine(Website, "books");

        public static string FilesRoot => UrlHelper.Combine(Website, "files");

        public static string GenresRoot => UrlHelper.Combine(Website, "genres");

        public static string AuthRoot => UrlHelper.Combine(Website, "auth");

        public static string PersonRoot => UrlHelper.Combine(Website, "persons");

        public static string FeedbackRoot => UrlHelper.Combine(Website, "feedbacks");

        public static string CoverUrl => UrlHelper.Combine(FilesRoot, "{bookId}/cover");

        public static string GetCoverUrl(int bookId, int? width = null, int? height = null)
        {
            var url = CoverUrl;
            if (width > 0 && height > 0)
            {
                url += $"?width={width}&height={height}";
            }
            url = url.Replace("{bookId}", bookId.ToString());
            return url;
        }
    }
}
