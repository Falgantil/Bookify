using System.ComponentModel;
using System.Runtime.CompilerServices;

using Bookify.App.Core.Services;
using Bookify.App.Sdk.Interfaces;

namespace Bookify.App.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}