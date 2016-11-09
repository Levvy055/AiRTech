using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AiRTech.Views;
using Xamarin.Forms;

namespace AiRTech
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            try
            {
                MainPage = new MasterDetailPage()
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(new MainPage())
                    {
                        BarBackgroundColor = Color.Blue
                    }
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
