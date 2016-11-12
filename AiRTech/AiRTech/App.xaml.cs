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
                var page = new MasterDetailPage
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(),
                    IsPresented = false
                };
                MainPage = page;
                ChangePageTo(typeof(MainPage), "AiRTech", false);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }


        public async void ChangePageTo(Type page, string title, bool inner = true)
        {
            var mPage = MainPage as MasterDetailPage;
            if (mPage == null)
            {
                return;
            }
            var newPage = GetPage(page, title);
            if (newPage != null)
            {
                if (inner)
                {
                    await NavPage.PushAsync(newPage);
                }
                else
                {
                    NavPage = new NavigationPage(newPage)
                    {
                        Title = "MainDetailNavPage",
                        BarBackgroundColor = Color.Blue
                    };
                    mPage.Detail = NavPage;
                }
            }
            mPage.IsPresented = false;
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

        private Page GetPage(Type page, string title = null)
        {
            Page newPage;
            if (CreatedPages.ContainsKey(page))
            {
                newPage = CreatedPages[page];
            }
            else
            {
                newPage = Activator.CreateInstance(page) as Page;
                if (title != null && newPage != null)
                {
                    newPage.Title = title;
                }
            }
            return newPage;
        }

        private Dictionary<Type, Page> CreatedPages { get; } = new Dictionary<Type, Page>();
        private NavigationPage NavPage { get; set; }
    }
}
