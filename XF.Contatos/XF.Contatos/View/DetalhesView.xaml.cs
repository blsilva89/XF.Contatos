using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Contatos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesView : ContentPage
    {
        ViewModel.ViewModelDetalhesContato vmDetalheContato;

        public DetalhesView(Model.Contato contato)
        {
            InitializeComponent();

            if (vmDetalheContato is null)
                vmDetalheContato = new ViewModel.ViewModelDetalhesContato(contato);

            BindingContext = vmDetalheContato;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var telefone = (Model.Telefone)e.Item;

            if (telefone != null)
            {
                if (await DisplayAlert("Ligando...", "Ligar para " + telefone.Numero + "?", "Sim", "Não"))
                {
                    vmDetalheContato.LigarParaContato(telefone.Numero);
                }
            }
            else
            {
                await this.DisplayAlert("Aviso", "Número não encontrado", "Ok");
            }
        }
    }
}