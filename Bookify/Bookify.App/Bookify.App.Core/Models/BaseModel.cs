using System.ComponentModel;
using System.Runtime.CompilerServices;

using Bookify.App.Core.Annotations;

namespace Bookify.App.Core.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}