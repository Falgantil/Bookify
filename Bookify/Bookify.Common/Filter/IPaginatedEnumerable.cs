using System.Collections;
using System.Collections.Generic;

namespace Bookify.Common.Filter
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    public interface IPaginatedEnumerable : IEnumerable
    {
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        int TotalCount { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.IEnumerable" />
    public interface IPaginatedEnumerable<T> : IPaginatedEnumerable, IEnumerable<T>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    /// <seealso cref="Bookify.Common.Filter.IPaginatedEnumerable{T}" />
    public class PaginatedEnumerable<T> : List<T>, IPaginatedEnumerable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedEnumerable{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="totalCount">The total count.</param>
        public PaginatedEnumerable(IEnumerable<T> collection, int totalCount) : base(collection)
        {
            this.TotalCount = totalCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedEnumerable{T}"/> class.
        /// </summary>
        /// <param name="totalCount">The total count.</param>
        public PaginatedEnumerable(int totalCount)
        {
            this.TotalCount = totalCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedEnumerable{T}"/> class.
        /// </summary>
        public PaginatedEnumerable()
        {
            
        }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }
    }
}
