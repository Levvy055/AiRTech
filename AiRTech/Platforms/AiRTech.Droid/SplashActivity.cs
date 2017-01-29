using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Util;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

namespace AiRTech.Droid
{
    [Activity(Label = "AiR Tech", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        private bool _initialized;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Log.Debug(TAG, "SplashActivity.OnCreate");
            if (!_initialized)
            {
                Inititalize(bundle);
            }
            if (StartupWork == null)
            {
                StartupWork = new Task(() =>
                {
                    Log.Debug(TAG, "Performing some startup work that takes a bit of time.");

                    //Task.Delay(5000);
                    Log.Debug(TAG, "Working in the background.");
                });

                StartupWork.ContinueWith(t =>
                {
                    Log.Debug(TAG, "Work is finished - start MainActivity.");
                    var intent = new Intent(Application.Context, typeof(MainActivity));
                    StartActivity(intent);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartupWork.Start();
        }

        private void Inititalize(Bundle bundle)
        {
            Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            var app = new App();
            _initialized = true;
        }

        public Task StartupWork { get; set; }
    }
}