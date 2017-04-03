using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MoneyConverter.Models;
using Xamarin.Forms;
using static System.Convert;

namespace MoneyConverter.Views
{
    public partial class MoedasPage : ContentPage
    {
        public ObservableCollection<ValorInfo> ListaMoedas { get; set; }

        public MoedasPage()
        {
            InitializeComponent();
            ListaMoedas = new ObservableCollection<ValorInfo>();
            GetMoedas();
            CarregaMoedasPicker();
        }
        private async void GetMoedas()
        {
            var repository = new Repository();
            var itens = await repository.DeserializeJson();

            foreach (var item in itens.Valores)
            {
                ListaMoedas.Add(item.Value);
            }
        }

        private void CarregaPicker(Picker picker)
        {
            foreach (var itens in ListaMoedas)
            {
                picker.Items.Add(itens.Nome);
            }
        }

        private void CarregaMoedasPicker()
        {
            CarregaPicker(moedaPicker);
        }

        private async void btnConverter_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(valor.Text))
            {
                await DisplayAlert("Erro!", "Informe o valor para realizar a conversão.", "Voltar");
                return;
            }
            if (moedaPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Erro!", "Selecione uma moeda para realizar a conversão.", "Voltar");
                return;
            }

            var valorConversao = decimal.Parse(valor.Text);
            var taxa = await GetTaxa(moedaPicker.SelectedIndex);
            var valorConvertido = valorConversao * ToDecimal(taxa);

            lblmsg.Text = $"{valorConversao:N2} {moedaPicker.Items[moedaPicker.SelectedIndex]} = R$ {valorConvertido:N2}";
        }

        private static async Task<double> GetTaxa(int index)
        {
            var repository = new Repository();
            var itens = await repository.DeserializeJson();

            var listaValores = itens.Valores.Select(item => item.Value.Valor).ToList();
            
            return listaValores[index];
        }

        private async void MenuItem1_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ContatoPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro!", ex.Message, "Cancelar");
            }
        }
    }
}
