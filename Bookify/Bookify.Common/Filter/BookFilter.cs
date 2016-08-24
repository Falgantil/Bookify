namespace Bookify.Common.Filter
{
    public class BookFilter : BaseFilter
    {
        public bool MyBooks { get; set; }

        public int[] Genres { get; set; }

        public int? Author { get; set; }

        public string OrderBy { get; set; } = "Price";

        public bool Descending { get; set; } = false;
        public int? PersonId { get; set; }
    }
}