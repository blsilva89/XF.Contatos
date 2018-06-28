using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Contatos.ViewModel;

namespace XF.Contatos
{
    public partial class MainPage : ContentPage
    {
        ViewModelContatos vmContatos;
        public MainPage()
        {
            if (vmContatos is null)
                vmContatos = new ViewModelContatos();


            InitializeComponent();
            this.BindingContext = vmContatos;
        }
    }
}
