using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Contatos.Media;
using XF.Contatos.Model;
using XF.Contatos.View;

namespace XF.Contatos.ViewModel
{
    public class ViewModelContatos : INotifyPropertyChanged
    {

        public ICommand NavegarParaDetalhesCommand { get; private set; }
        public ObservableCollection<Contato> AgendaDeContatos { get; set; } = new ObservableCollection<Contato>();        

        public ViewModelContatos()
        {
            DefinirComandos();
            CarregarContatos();
        }

        private void DefinirComandos()
        {
            NavegarParaDetalhesCommand = new Command((param) =>
            {
                App.Current.MainPage.Navigation.PushAsync(new DetalhesView(param as Contato));
            });
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CarregarContatos()
        {
            MessagingCenter.Subscribe<IContatos, Contato>
                (this, "contatos", (objeto, con) =>
                {
                    AgendaDeContatos
                        .Add(new Contato()
                        {
                            Nome = con.Nome,
                            Sobrenome = con.Sobrenome,
                            Telefones = con.Telefones
                        });
                });

            IContatos contatos = DependencyService.Get<IContatos>();
            contatos.GetContatos();
        }
    }
}
