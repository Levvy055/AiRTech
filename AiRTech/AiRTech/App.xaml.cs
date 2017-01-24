using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AiRTech.Core;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Net;
using AiRTech.Core.Subjects;
using AiRTech.Views;
using AiRTech.Views.Other;
using AiRTech.Views.SubjectData;
using Xamarin.Forms;
using DefinitionsPage = AiRTech.Views.Pages.DefinitionsPage;
using MainPage = AiRTech.Views.Pages.MainPage;
using MenuPage = AiRTech.Views.Pages.MenuPage;
using SubjectPage = AiRTech.Views.Pages.SubjectPage;

namespace AiRTech
{
    public partial class App : AiRTechApp
    {
        private readonly Color _mainBgColor = Color.FromRgb(169, 169, 169);
        private readonly Color _menuBgColor = Color.FromRgb(95, 158, 160);
        private readonly Color _topBarColor = Color.FromRgb(95, 158, 160);
        private readonly Color _topBarTextColor = Color.FromRgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);

        public App() : base()
        {
            var mainContentPage = new ContentPage
            {
                Title = "Ładowanie",
                Content = new ActivityIndicator
                {
                    IsRunning = true,
                    Color = Color.DarkRed
                }
            };
            NavPage = new NavigationPage(mainContentPage)
            {
                BarBackgroundColor = _topBarColor,
                BarTextColor = _topBarTextColor,
                BackgroundColor = _mainBgColor
            };
            MainPage = new MasterDetailPage
            {
                Master = new MenuPage
                {
                    BackgroundColor = _menuBgColor,
                    IsBusy = true,
                    IsDisabled = true
                },
                Detail = NavPage,
                MasterBehavior = MasterBehavior.Popover
            };
        }

        private async void NavigateTo(Type page, string title, bool inner = true, params object[] args)
        {
            var mPage = MainPage as MasterDetailPage;
            if (mPage == null)
            {
                return;
            }
            var newPage = GetPage(page, title, args);
            if (newPage != null)
            {
                if (newPage.GetType() == typeof(SolverPage))
                {
                    var solverPage = newPage as SolverPage;
                    solverPage.NavigateToMain();
                }
                else
                {
                    if (inner)
                    {
                        await NavPage.PushAsync(newPage);
                    }
                    else
                    {
                        NavPage = new NavigationPage(newPage)
                        {
                            Title = newPage.Title,
                            BarBackgroundColor = _topBarColor,
                            BarTextColor = _topBarTextColor,
                            BackgroundColor = _mainBgColor
                        };
                        mPage.Detail = NavPage;
                    }
                }
                mPage.IsPresented = false;

            }
        }

        private async void NavigateTo(Page page, bool removePrevious = false)
        {
            if (removePrevious)
            {
                await NavPage.PopAsync(false);
            }
            if (NavPage.CurrentPage != page)
            {
                await NavPage.PushAsync(page);
            }
            else
            {
                NavPage.Title = page.Title;
            }
        }

        public override async void NavigateToModal(ContentPage modal)
        {
            await NavPage.PushAsync(modal);
        }

        public Page GetPage(Type pageType, string title = null, params object[] args)
        {
            Page page;
            if (MainPages.ContainsKey(pageType))
            {
                if (pageType == typeof(SubjectPage) || pageType == typeof(DefinitionsPage) || pageType == typeof(SolverPage))
                {
                    page = GetPage(pageType, args);
                }
                else
                {
                    page = MainPages[pageType][0];
                }
            }
            else
            {
                page = CreatePage(pageType, args);
            }
            if (!string.IsNullOrWhiteSpace(title) && page != null)
            {
                page.Title = title;
            }
            return page;
        }

        private Page GetPage(Type pageType, object[] args)
        {
            var pages = MainPages[pageType];
            Page page = null;
            foreach (var p in pages)
            {
                dynamic sp = p;
                if (sp.Subject == args[0])
                {
                    page = p;
                }
            }
            return page ?? CreatePage(pageType, args);
        }

        private Page CreatePage(Type pageType, object[] args)
        {
            Page page;
            if (args == null || args.Length == 0)
            {
                page = Activator.CreateInstance(pageType) as Page;
            }
            else
            {
                page = Activator.CreateInstance(pageType, args) as Page;
            }
            var list = new List<Page> { page };
            MainPages[pageType] = list;
            return page;
        }

        protected override async void OnStart()
        {
            //await Task.Delay(5000);
            try
            {
                DialogManager = new DialogManager();
                DataCore = new CoreManager(this);
                NavigateTo(typeof(MainPage), "AiRTech", false);
                var menuPage = (MenuPage)((MasterDetailPage)MainPage).Master;
                menuPage.IsBusy = false;
                menuPage.IsDisabled = false;
#if DEBUG
                //NavigateTo(typeof(SubjectsPage), "Subjects", false);
                //var s = Subject.Subjects[SubjectType.PODSTAWY_TEORII_SYGNALOW];
                //NavigateTo(typeof(SubjectPage), "Podstawy Teorii Sygnałów", true, s);
                //NavigateTo(typeof(DefinitionsPage), "Podstawy Teorii Sygnałów", true, s);
                //NavigateTo(typeof(SolverPage), "Podstawy Teorii Sygnałów", true, s);
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

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void OnDestroy()
        {
            MainPage = new ContentPage();
        }

        public CoreManager DataCore { get; set; }
        private Dictionary<Type, List<Page>> MainPages { get; } = new Dictionary<Type, List<Page>>();
        private NavigationPage NavPage { get; set; }
    }
}
