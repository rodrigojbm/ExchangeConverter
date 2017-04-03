using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoneyConverter.ViewModels
{
    public class ContatoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ContatoViewModel()
        {
            
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
