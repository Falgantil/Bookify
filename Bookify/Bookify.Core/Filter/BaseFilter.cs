namespace Bookify.Core.Filter
{
    public abstract class BaseFilter
    {
        public int Index { get; set; }
        public int Count { get; set; } = 10;
    }
}