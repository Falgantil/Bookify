using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;

namespace Bookify.App.Core.Collections
{
    public class ObservableServiceCollection<TModel, TFilter, IService> : ObservableCollection<TModel>
        where TFilter : BaseFilter, new()
        where IService : IGetByFilterService<TModel, TFilter>
    {
        private readonly IService service;

        public ObservableServiceCollection(IService service, TFilter filter = null)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            this.service = service;
            this.Filter = filter ?? new TFilter();
        }

        public ObservableServiceCollection(IService service, IEnumerable<TModel> collection, TFilter filter = null) : base(collection)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            this.service = service;
            this.Filter = filter ?? new TFilter();
        }

        public TFilter Filter { get; }

        public bool ReachedBottom { get; set; }

        public bool IsLoading { get; private set; }

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

        public async Task Restart()
        {
            this.Filter.Skip = 0;
            this.Items.Clear();
            await this.LoadMore();
        }
    }
}