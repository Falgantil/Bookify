namespace Bookify.Core.Filter
{
    public class BookFilter : BaseFilter
    {
        public int[] GenreIds { get; set; }

        public string Search { get; set; }
        public string OrderBy { get; set; } = "Price";
        public bool Desc { get; set; } = false;
    }
}