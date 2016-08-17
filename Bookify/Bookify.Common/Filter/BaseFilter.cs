namespace Bookify.Common.Filter
{
    public abstract class BaseFilter
    {
        public int Index { get; set; }

        public int Count { get; set; } = 10;

        public string SearchText { get; set; }
    }
}