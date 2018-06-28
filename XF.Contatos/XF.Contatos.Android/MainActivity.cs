using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Xamarin.Forms;

namespace XF.Contatos.Droid
{
    [Activity(Label = "XF.Contatos", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            var activity = MainApplication.CurrentContext as Activity;
            var uri = Android.Net.Uri.Parse(activity.Intent.Extras.Get("output").ToString());
            //var uri = Android.Net.Uri.Parse(data.Extras.Get("output").ToString());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                this.ContentResolver.OpenInputStream(uri).CopyTo(ms);

                byte[] bytes = new byte[ms.Length];
                bytes = ms.ToArray();

                MessagingCenter.Send<byte[]>(bytes, "fotoTirada");
            }
        }
    }
}

