using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Geolocation;
using XF.Contatos.Droid;
using XF.Contatos.Media;
using XF.Contatos.Model;

[assembly: Dependency(typeof(GeoLocation_Android))]
namespace XF.Contatos.Droid
{
    public class GeoLocation_Android : ILocalizacao
    {
        public void GetCoordenada()
        {
            var context = MainApplication.CurrentContext as Activity;
            var locator = new Geolocator(context) { DesiredAccuracy = 50 };

            locator.GetPositionAsync(timeout: 10000).ContinueWith(t =>
            {
                SetCoordenada(t.Result.Latitude, t.Result.Longitude);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void VerNoMapa(string longitude, string latitude)
        {
            var activity = MainApplication.CurrentContext as Activity;
            var packageManager = MainApplication.CurrentContext.PackageManager;

            //Dados Fake
            latitude = "-23.746461";
            longitude = "-46.601818";

            Android.Net.Uri uriPin = Android.Net.Uri.Parse($"geo:{latitude},{longitude}?q={latitude},{longitude}");
            Android.Net.Uri uriIntent = Android.Net.Uri.Parse($"geo:{latitude},{longitude}?z=zoom");
            activity.Intent = new Intent(Intent.ActionView, uriPin);
            activity.Intent.SetPackage("com.google.android.apps.maps");

            if (activity.Intent.ResolveActivity(packageManager) != null)
            {
                activity.StartActivity(activity.Intent);
            }
        }

        void SetCoordenada(double paramLatitude, double paramLongitude)
        {
            var coordenada = new Coordenada()
            {
                Latitude = paramLatitude.ToString(),
                Longitude = paramLongitude.ToString()
            };

            MessagingCenter.Send<ILocalizacao, Coordenada>
                 (this, "coordenada", coordenada);
        }

        private bool GetPermissions()
        {
            var activity = MainApplication.CurrentContext as Activity;

            bool ok = MainApplication.CurrentContext.CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) == Permission.Granted &&
                        MainApplication.CurrentContext.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted;
            if (!ok)
                ActivityCompat.RequestPermissions(activity, new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessCoarseLocation }, 100);

            return ok;
        }
    }
}