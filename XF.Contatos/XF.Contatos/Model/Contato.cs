using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XF.Contatos.Model
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public byte[] fotoPerfoç { get; set; }

        public IList<Telefone> Telefones { get; set; }
    }

    public class Telefone
    {
        public string Rotulo { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
    }
}
