namespace Bookify.Common.Filter
{
    public class BookFilter : BaseFilter
    {
        public int[] GenreIds { get; set; }
        
        public string OrderBy { get; set; } = "Price";

        public bool Descending { get; set; } = false;
    }
}