namespace Bookify.Common.Filter
{
    public abstract class BaseFilter
    {
        public int Skip { get; set; }

        public int Take { get; set; } = 10;

        public string Search { get; set; }
    }
}