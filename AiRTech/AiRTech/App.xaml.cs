﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Subjects;
using AiRTech.Core.Web;
using AiRTech.Views;
using AiRTech.Views.SubjectData;
using Xamarin.Forms;

namespace AiRTech
{
    public partial class App : Application
    {
        public App()
        {
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
#if DEBUG
                ChangePageTo(typeof(AboutPage), "Ooo", false);
                //var s = Subject.Subjects[SubjectType.PODSTAWY_TEORII_SYGNALOW];
                //ChangePageTo(typeof(SubjectPage), "Podstawy Teorii Sygnałów", true, s);
                //ChangePageTo(typeof(SolverPage), "Podstawy Teorii Sygnałów", true, s);
                //var np = GetPage(typeof(SolverPage), "Podstawy Teorii Sygnałów", s) as SolverPage;
                //np?.NavigateTo(3);
#else
                ((MasterDetailPage)MainPage).IsPresented = true;
#endif
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

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

        public async void NavigateToModal(ContentPage detailPage)
        {
            await NavPage.PushAsync(detailPage);
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
            }
            if (title != null && newPage != null)
            {
                newPage.Title = title;
            }
            return newPage;
        }

        public IDbHandler Database { get; set; }

        private Dictionary<Type, Page> CreatedPages { get; } = new Dictionary<Type, Page>();
        private NavigationPage NavPage { get; set; }
        public WebCore Web { get; set; }
    }
}
