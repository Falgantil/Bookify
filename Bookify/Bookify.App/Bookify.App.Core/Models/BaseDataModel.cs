using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bookify.App.Core.Models
{
    public class BaseDataModel : BaseModel
    {
        public int Id { get; set; }
    }

    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}