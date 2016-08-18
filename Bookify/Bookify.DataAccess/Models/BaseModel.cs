using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bookify.DataAccess.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}