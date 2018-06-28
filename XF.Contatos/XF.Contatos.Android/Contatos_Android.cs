using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Contacts;
using Xamarin.Forms;
using XF.Contatos.Media;
using XF.Contatos.Droid;
using XF.Contatos.Model;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;

[assembly: Dependency(typeof(Contatos_Android))]
namespace XF.Contatos.Droid
{
    public class Contatos_Android : IContatos
    {
        public void GetContatos()
        {
            var context = Android.App.Application.Context;
            var uri = ContactsContract.Contacts.ContentUri;

            var agendaDeContatos = new AddressBook(context) { PreferContactAggregation = true };

            if (!GetPermissions())
                return;


            foreach (var contato in agendaDeContatos)
            {
                MessagingCenter.Send<IContatos, Contato>(this, "contatos", new Contato { Nome = contato.FirstName, Sobrenome = contato.LastName, Telefones = contato.Phones.Select(p => new Telefone() { Numero = p.Number, Rotulo = p.Label }).ToList() });
            }
        }

        private bool GetPermissions()
        {
            var activity = MainApplication.CurrentContext as Activity;

            bool ok = MainApplication.CurrentContext.CheckSelfPermission(Manifest.Permission.ReadContacts) == Permission.Granted;
            if (!ok)
                ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.ReadContacts }, 100);

            return ok;
        }
    }
}