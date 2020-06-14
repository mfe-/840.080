using Android.App;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using DDILibrary;
using Java.IO;
using Prism;
using Prism.Ioc;
using System;

namespace DDIApp.Droid
{
    [Activity(Label = "DDIApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer(this)));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        private MainActivity _mainActivity;

        public AndroidInitializer(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            DrugInteractionService drugInteractionService = new DrugInteractionService(GetCsvStream);
            containerRegistry.RegisterInstance(drugInteractionService);
        }
        public System.IO.Stream GetCsvStream()
        {
            return _mainActivity.Assets.Open("CombinedDatasetConservativeTWOSIDES.csv");
        }
    }

}

