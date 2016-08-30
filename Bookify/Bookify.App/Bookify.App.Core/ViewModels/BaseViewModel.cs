using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bookify.App.Sdk.Exceptions;
using Polly;
// ReSharper disable MemberCanBeMadeStatic.Global

namespace Bookify.App.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Attempts to execute the provided operation. If it fails, waits one second and retries. If it fails again, throws the final exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="op">The op.</param>
        /// <returns></returns>
        protected async Task<T> TryTask<T>(Func<Task<T>> op)
        {
            return await Policy
                .Handle<WebException>()
                .Or<HttpResponseException>()
                .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1) })
                .ExecuteAsync(op);
        }

        /// <summary>
        /// See <see cref="TryTask{T}"/>.
        /// </summary>
        /// <param name="op">The op.</param>
        /// <returns></returns>
        protected async Task TryTask(Func<Task> op)
        {
            await this.TryTask(async () =>
            {
                await op();
                return true;
            });
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            this.PropertyChanged = null;
        }
    }
}