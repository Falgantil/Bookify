using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bookify.App.Core.Services;
using Bookify.App.Sdk.Exceptions;
using Bookify.App.Sdk.Interfaces;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        protected async Task<T> TryTask<T>(Func<Task<T>> op)
        {
            return await Policy
                .Handle<WebException>()
                .Or<HttpResponseException>()
                .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1) })
                .ExecuteAsync(op);
        }

        protected async Task TryTask(Func<Task> op)
        {
            await this.TryTask(async () =>
            {
                await op();
                return true;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {
            this.PropertyChanged = null;
        }
    }
}