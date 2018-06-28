using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Contatos.Media;
using XF.Contatos.Model;

namespace XF.Contatos.ViewModel
{
    public class ViewModelDetalhesContato : INotifyPropertyChanged
    {
        public ICommand TirarFotoCommand { get; private set; }
        public ICommand VerNoMapaCommand { get; private set; }
        public ILocalizacao localizacao { get; set; }

        private ImageSource fotoContato = "icon.png";

        public ImageSource FotoContato
        {
            get
            { return fotoContato; }
            private set
            {
                fotoContato = value;
                OnPropertyChanged();
            }
        }

        private Contato contato;
        public Contato Contato
        {
            get { return contato; }
            private set
            {
                contato = value;
                OnPropertyChanged();
            }
        }

        private Coordenada coordenada;
        public Coordenada Coordenada
        {
            get { return coordenada; }
            set
            {
                coordenada = value;
                OnPropertyChanged();
            }
        }

        public ViewModelDetalhesContato(Contato contato)
        {
            this.Contato = contato;
            DefinirComandos();

            MessagingCenter.Subscribe<ILocalizacao, Coordenada>
                 (this, "coordenada", (obj, geo) =>
                 {
                     Coordenada = geo;
                 });

            localizacao = DependencyService.Get<ILocalizacao>();
            localizacao.GetCoordenada();
        }

        private void DefinirComandos()
        {
            TirarFotoCommand = new Command(() =>
            {
                ICamera camera = DependencyService.Get<ICamera>();
                camera.TirarFoto();

                MessagingCenter.Subscribe<byte[]>(this, "fotoTirada", (streamFoto) =>
                {
                    FotoContato = ImageSource.FromStream(() => new MemoryStream(streamFoto));
                });
            });

            VerNoMapaCommand = new Command(() => {
                localizacao.VerNoMapa(Coordenada.Longitude, Coordenada.Latitude);
            });
        }

        public void LigarParaContato(string numero)
        {
            ILigar ligar = DependencyService.Get<ILigar>();
            ligar.Discar(numero);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
