using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;

namespace Bookify.App.Core.Collections
{
    /// <summary>
    /// An observable collection that you feed a Service to, and it will handle pagination all by itself. Magic!
    /// <para>Disclaimer: It is, in fact, not really magic. Just code being artfully stitched together to create a fancy and very useful class!</para>
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TFilter">The type of the filter.</typeparam>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{TModel}" />
    public class ObservableServiceCollection<TModel, TFilter, TService> : ObservableCollection<TModel>
        where TFilter : BaseFilter, new()
        where TService : IGetByFilterService<TModel, TFilter>
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly TService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableServiceCollection{TModel, TFilter, TService}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="filter">The filter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public ObservableServiceCollection(TService service, TFilter filter = null)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            this.service = service;
            this.Filter = filter ?? new TFilter();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableServiceCollection{TModel, TFilter, TService}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="filter">The filter.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public ObservableServiceCollection(TService service, IEnumerable<TModel> collection, TFilter filter = null) : base(collection)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            this.service = service;
            this.Filter = filter ?? new TFilter();
        }

        /// <summary>
        /// Gets the filter. Edit this with whatever extra data you want to search by.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public TFilter Filter { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this collection has retrieved the bottom of the list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reached bottom; otherwise, <c>false</c>.
        /// </value>
        public bool ReachedBottom { get; set; }

        /// <summary>
        /// Gets a value indicating whether this collection is currently loading data.
        /// </summary>
        /// <value>
        /// <c>true</c> if this collection is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading { get; private set; }

        /// <summary>
        /// Adds the range of items to the collection.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddRange(IEnumerable<TModel> items)
        {
            var enumerable = items.ToArray();
            foreach (var model in enumerable)
            {
                this.Items.Add(model);
            }
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, enumerable));
        }

        /// <summary>
        /// Attemps to load more data, based on the current filter settings and content of this collection.
        /// This is where the real magic happens. If either it's already loading, or it has already reached the bottom, it will return immediately.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task LoadMore()
        {
            if (this.IsLoading || this.ReachedBottom)
            {
                return;
            }

            this.IsLoading = true;

            try
            {
                this.Filter.Skip = this.Count;
                var items = await this.service.GetItems(this.Filter);
                if (items == null) throw new ArgumentNullException(nameof(items));
                var list = items.ToList();
                foreach (var model in list)
                {
                    this.Items.Add(model);
                }
                this.ReachedBottom = this.Count == items.TotalCount;
                this.Filter.Skip = this.Count;

                this.IsLoading = false;

                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list));
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        /// <summary>
        /// Restarts this collection, setting the search index back to 0, and clearing the collection.
        /// <para>Also initiates a <see cref="LoadMore"/> call.</para>
        /// </summary>
        /// <returns></returns>
        public async Task Restart()
        {
            this.ReachedBottom = false;
            this.Filter.Skip = 0;
            this.Items.Clear();
            await this.LoadMore();
        }
    }
}