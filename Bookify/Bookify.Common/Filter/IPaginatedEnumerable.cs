using System.Collections;
using System.Collections.Generic;

namespace Bookify.Common.Filter
{
    public interface IPaginatedEnumerable : IEnumerable
    {
        int TotalCount { get; set; }
    }

    public interface IPaginatedEnumerable<T> : IPaginatedEnumerable, IEnumerable<T>
    {

    }

    public class PaginatedEnumerable<T> : List<T>, IPaginatedEnumerable<T>
    {
        public PaginatedEnumerable(IEnumerable<T> collection, int totalCount) : base(collection)
        {
            this.TotalCount = totalCount;
        }

        public PaginatedEnumerable(int totalCount)
        {
            this.TotalCount = totalCount;
        }

        public PaginatedEnumerable()
        {
            
        }

        public int TotalCount { get; set; }
    }
}
