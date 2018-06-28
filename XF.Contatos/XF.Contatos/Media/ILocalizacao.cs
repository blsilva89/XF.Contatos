using System;
using System.Collections.Generic;
using System.Text;

namespace XF.Contatos.Media
{
    public interface ILocalizacao
    {
        void GetCoordenada();

        void VerNoMapa(string longitude, string latitude);
    }
}
