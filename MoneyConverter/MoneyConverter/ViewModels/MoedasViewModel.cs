using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoneyConverter.Models;
using Xamarin.Forms;

namespace MoneyConverter.ViewModels
{
    public class MoedasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool Busy;
        public bool IsBusy
        {
            get
            {
                return Busy;
            }
            set
            {
                Busy = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ValorInfo> ListaMoedas { get; set; }

        public MoedasViewModel()
        {
            ListaMoedas = new ObservableCollection<ValorInfo>();
            GetMoedas();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        async void GetMoedas()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var repository = new Repository();
                    var itens = await repository.DeserializeJson();
                    ListaMoedas.Clear();

                    foreach (var item in itens.Valores)
                    {
                        ListaMoedas.Add(item.Value);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                }

                if (Error != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", Error.Message, "OK");
                }
            }
        }
    }
}
