using System.Collections.Generic;

namespace Bookify.DataAccess.Models.ViewModels
{
    public interface IPaginatedEnumerable<T> : IEnumerable<T>
    {
        int TotalCount { get; set; }
        int Index { get; set; }
        int Count { get; set; }
    }

    public class PaginatedEnumerable<T> : List<T>, IPaginatedEnumerable<T>
    {
        public PaginatedEnumerable(IEnumerable<T> collection, int totalCount, int index, int count)
            : base(collection)
        {
            this.TotalCount = totalCount;
            this.Index = index;
            this.Count = count;
        }

        public PaginatedEnumerable(int totalCount, int index, int count)
        {
            this.TotalCount = totalCount;
            this.Index = index;
            this.Count = count;
        }

        public int TotalCount { get; set; }

        public int Index { get; set; }

        public int Count { get; set; }
    }
}
