using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.Annotations;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;

namespace Bookify.App.Core.Collections
{
    public class ObservableApiCollection<TModel, TFilter, IApi> : ObservableCollection<TModel>
        where TFilter : BaseFilter, new()
        where IApi : IGetByFilterApi<TModel, TFilter>
    {
        private readonly IApi api;

        public ObservableApiCollection([NotNull] IApi api, TFilter filter = null)
        {
            if (api == null)
                throw new ArgumentNullException(nameof(api));
            this.api = api;
            this.Filter = filter ?? new TFilter();
        }

        public ObservableApiCollection([NotNull] IApi api, IEnumerable<TModel> collection, TFilter filter = null) : base(collection)
        {
            if (api == null)
                throw new ArgumentNullException(nameof(api));
            this.api = api;
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
                this.Filter.Index = this.Count;
                var items = await this.api.GetItems(this.Filter);
                var list = items.ToList();
                foreach (var model in list)
                {
                    this.Items.Add(model);
                }
                this.ReachedBottom = this.Count == items.TotalCount;
                this.Filter.Index = this.Count;

                this.IsLoading = false;

                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list));
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}