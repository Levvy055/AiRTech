using System;
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
        private readonly Color _mainBgColor = Color.FromRgb(169, 169, 169);
        private readonly Color _menuBgColor = Color.FromRgb(95, 158, 160);
        private readonly Color _topBarColor = Color.FromRgb(95, 158, 160);
        private readonly Color _topBarTextColor = Color.FromRgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);

        public App()
        {
            try
            {
                MainPage = new MasterDetailPage
                {
                    Master = new MenuPage { BackgroundColor = _menuBgColor },
                    Detail = new NavigationPage(),
                    MasterBehavior = MasterBehavior.Popover
                };
                try
                {
                    FileHandler = DependencyService.Get<IFileHandler>();
                    FileHandler.Init();
                }
                catch (Exception e)
                {
                    MainPage.DisplayAlert("Error!", "Błąd uzyskiwania dostępu do pliku bazy!", "Zamknij");
                    Debug.WriteLine(e);
                    throw;
                }
                try
                {
                    Web = new WebCore();
                }
                catch (Exception e)
                {
                    MainPage.DisplayAlert("Offline!", "Brak dostępu do serwera!", "Zamknij");
                    Debug.WriteLine(e);
                }
                ChangePageTo(typeof(MainPage), "AiRTech", false);
#if DEBUG
                //ChangePageTo(typeof(SubjectsPage), "Subjects", false);
                //var s = Subject.Subjects[SubjectType.PODSTAWY_TEORII_SYGNALOW];
                //ChangePageTo(typeof(SubjectPage), "Podstawy Teorii Sygnałów", true, s);
                //ChangePageTo(typeof(DefinitionsPage), "Podstawy Teorii Sygnałów", true, s);
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
                if (newPage.GetType() == typeof(SolverPage))
                {
                    var s = newPage as SolverPage;
                    s.NavigateToMain();
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

        public async void ChangePageTo(Page page, bool removePrevious = false)
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

        public async void NavigateToModal(ContentPage detailPage)
        {
            await NavPage.PushAsync(detailPage);
        }

        public Page GetPage(Type pageType, string title = null, params object[] args)
        {
            Page page;
            if (CreatedPages.ContainsKey(pageType))
            {
                page = CreatedPages[pageType];
            }
            else
            {
                if (args == null || args.Length == 0)
                {
                    page = Activator.CreateInstance(pageType) as Page;
                }
                else
                {
                    page = Activator.CreateInstance(pageType, args) as Page;
                }
                CreatedPages.Add(pageType, page);
            }
            if (!string.IsNullOrWhiteSpace(title) && page != null)
            {
                page.Title = title;
            }
            return page;
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

        public void OnDestroy()
        {
            MainPage = new ContentPage();
        }

        private Dictionary<Type, Page> CreatedPages { get; } = new Dictionary<Type, Page>();
        private NavigationPage NavPage { get; set; }
        public IFileHandler FileHandler { get; set; }
        public WebCore Web { get; set; }
    }
}
