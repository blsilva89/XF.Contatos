using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Media;
using XF.Contatos.Droid;
using XF.Contatos.Media;

[assembly: Dependency(typeof(Camera_Android))]
namespace XF.Contatos.Droid
{
    public class Camera_Android : ICamera
    {
        readonly string[] Permissoes =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.Camera
        };

        const int RequestLocationId = 100;

        public void TirarFoto()
        {

            if (GetPermissions())
            {
                var activity = MainApplication.CurrentContext as Activity;
                activity.Intent = new Intent(MediaStore.ActionImageCapture);
                Android.Net.Uri fotoURI = FileProvider.GetUriForFile(MainApplication.CurrentContext, "com.companyname.XF.Contatos.fileprovider", PegarArquivoImagem());
                activity.Intent.PutExtra(MediaStore.ExtraOutput, fotoURI);
                activity.StartActivityForResult(activity.Intent, RequestLocationId);
            }
        }

        private static Java.IO.File PegarArquivoImagem()
        {
            Java.IO.File imagePathContext = new Java.IO.File(MainApplication.CurrentContext.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures), "images");

            if (!imagePathContext.Exists())
                imagePathContext.Mkdirs();

            Java.IO.File arquivoImagemContext = new Java.IO.File(imagePathContext, "default_image_context.jpg");

            return arquivoImagemContext;
        }

        private bool GetPermissions()
        {
            var activity = MainApplication.CurrentContext as Activity;

            bool ok = MainApplication.CurrentContext.CheckSelfPermission(Manifest.Permission.Camera) == Permission.Granted &&
                    MainApplication.CurrentContext.CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Permission.Granted &&
                    MainApplication.CurrentContext.CheckSelfPermission(Manifest.Permission.WriteExternalStorage) == Permission.Granted;
            if (!ok)
                ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.Camera, Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 100);

            return ok;
        }
    }
}