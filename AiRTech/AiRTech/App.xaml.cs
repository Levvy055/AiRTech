using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Web;
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
                MainPage = new MasterDetailPage
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(),
                    MasterBehavior = MasterBehavior.Popover
                };
                ChangePageTo(typeof(MainPage), "AiRTech", false);
                DependencyService.Get<IFileHandler>().Init();
                Database = new DbHandler();
                Web = new WebCore(Database);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public WebCore Web { get; set; }

        public async void ChangePageTo(Type page, string title, bool inner = true, params object[] args)
        {
            var mPage = MainPage as MasterDetailPage;
            if (mPage == null)
            {
                return;
            }
            var newPage = GetPage(page, title, args);
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

        public Page GetPage(Type page, string title = null, params object[] args)
        {
            Page newPage;
            if (CreatedPages.ContainsKey(page))
            {
                newPage = CreatedPages[page];
            }
            else
            {
                if (args == null || args.Length == 0)
                {
                    newPage = Activator.CreateInstance(page) as Page;
                }
                else
                {
                    newPage = Activator.CreateInstance(page, args) as Page;
                }
                if (title != null && newPage != null)
                {
                    newPage.Title = title;
                }
            }
            return newPage;
        }

        public IDbHandler Database { get; set; }

        private Dictionary<Type, Page> CreatedPages { get; } = new Dictionary<Type, Page>();
        private NavigationPage NavPage { get; set; }
    }
}
