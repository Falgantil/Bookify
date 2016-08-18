using System.Collections;
using System.Collections.Generic;

namespace Bookify.Common.Filter
{
    public interface IPaginatedEnumerable : IEnumerable
    {
        int TotalCount { get; set; }
        int Index { get; set; }
        int Amount { get; set; }
    }

    public interface IPaginatedEnumerable<T> : IPaginatedEnumerable, IEnumerable<T>
    {

    }

    public class PaginatedEnumerable<T> : List<T>, IPaginatedEnumerable<T>
    {
        public PaginatedEnumerable(IEnumerable<T> collection, int totalCount, int index, int amount)
            : base(collection)
        {
            this.TotalCount = totalCount;
            this.Index = index;
            this.Amount = amount;
        }

        public PaginatedEnumerable(int totalCount, int index, int amount)
        {
            this.TotalCount = totalCount;
            this.Index = index;
            this.Amount = amount;
        }

        public int TotalCount { get; set; }

        public int Index { get; set; }

        public int Amount { get; set; }
    }
}
