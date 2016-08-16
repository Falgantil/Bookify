namespace Bookify.Core.Filter
{
    public class BookFilter : BaseFilter
    {
        public int[] GenreIds { get; set; }

        public string Search { get; set; }
    }
}