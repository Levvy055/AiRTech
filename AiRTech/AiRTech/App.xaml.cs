using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AiRTech.Core;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Net;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Solv;
using AiRTech.Solvers;
using AiRTech.Views;
using AiRTech.Views.Other;
using AiRTech.Views.Pages;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;
using DefinitionsPage = AiRTech.Views.Pages.DefinitionsPage;
using MainPage = AiRTech.Views.Pages.MainPage;
using MenuPage = AiRTech.Views.Pages.MenuPage;
using SolverView = AiRTech.Core.Subjects.Solv.SolverView;
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

        protected override async void OnStart()
        {
            //await Task.Delay(5000);
            try
            {
                DialogManager = new DialogManager();
                DataCore = new CoreManager(this);
                InitSolvers();
                NavigateToMain(typeof(MainPage), "AiRTech");
                var menuPage = (MenuPage)((MasterDetailPage)MainPage).Master;
                menuPage.IsBusy = false;
                menuPage.IsDisabled = false;
#if DEBUG
                NavigateToMain(typeof(SubjectsPage), "Subjects");
                var s = Subject.Subjects[SubjectType.PODSTAWY_TEORII_SYGNALOW];
                NavigateToSubject(s, "Podstawy Teorii Sygnałów");
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

        protected override void InitSolvers()
        {
            new ElectronicBasicsSolver().InitSolverTabs((s) =>
             {
                 foreach (var t in s.Tabs)
                 {
                     ViewHandler.Add(t, s.SubjectType);
                 }
             });
            new SignalTheoryBasicsSolver().InitSolverTabs((s) =>
            {
                foreach (var t in s.Tabs)
                {
                    ViewHandler.Add(t, s.SubjectType);
                }
            });
        }

        public override void NavigateToMain(Type pageType, string title)
        {
            NavigateTo(pageType, "AiRTech", false);
        }

        public override void NavigateToSubject(Subject subject, string title)
        {
            NavigateTo(typeof(SubjectPage), title, true, subject);
        }

        public override void NavigateToDefinition(string title, Subject subject)
        {
            NavigateTo(typeof(DefinitionsPage), title, true, subject);
        }

        public override void NavigateToFormula(string title, Subject subject)
        {
            NavigateTo(typeof(FormulasPage), title, true, subject);
        }

        public override void NavigateToSolver(Subject subject, string name, bool carousel = false)
        {
            var np = GetPage(typeof(SolverPage), subject.Name, subject) as SolverPage;
            NavigateToPage(np, carousel);
            if (!string.IsNullOrWhiteSpace(name))
            {
                var sv = ViewHandler.GetSolverView(subject.Base.SubjectType, name);
                np?.NavigateToTab(sv);
            }
            else
            {
                np?.NavigateToMain();
            }
        }

        public override void NavigateToSolver(Subject subject, SolverView sv)
        {
            var np = GetPage(typeof(SolverPage), subject.Name, subject) as SolverPage;
            NavigateToPage(np);
            np?.NavigateToTab(sv);
        }

        public override async void NavigateToModal(ContentPage modal)
        {
            await NavPage.PushAsync(modal);
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
                mPage.IsPresented = false;
            }
        }

        public override async void NavigateToPage(Page page, bool removePrevious = false)
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

        public Page GetPage(Type pageType, string title = null, params object[] args)
        {
            try
            {
                Page page;
                if (MainPages.ContainsKey(pageType))
                {
                    if (pageType == typeof(SubjectPage) || pageType == typeof(DefinitionsPage) ||
                        pageType == typeof(SolverPage))
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
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

        public CoreManager DataCore { get; set; }
        private Dictionary<Type, List<Page>> MainPages { get; } = new Dictionary<Type, List<Page>>();
        private NavigationPage NavPage { get; set; }
    }
}
