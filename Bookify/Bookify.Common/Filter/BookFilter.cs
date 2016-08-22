namespace Bookify.Common.Filter
{
    public class BookFilter : BaseFilter
    {
        public int[] Genres { get; set; }
        public int? Author { get; set; }
        
        public string OrderBy { get; set; } = "Price";

        public bool Descending { get; set; } = false;
    }
}