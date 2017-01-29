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
        private readonly Color _topBarTextColor = Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue);

        public App() : base()
        {
            var loadingPage = new ContentPage
            {
                Title = "Ładowanie",
                Content = new ActivityIndicator
                {
                    IsRunning = true,
                    Color = Color.DarkRed
                }
            };
            NavPage = new NavigationPage(loadingPage)
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
                NavigateToMain(NavPageType.MainPage, "AiRTech");
                var menuPage = (MenuPage)((MasterDetailPage)MainPage).Master;
                menuPage.IsBusy = false;
                menuPage.IsDisabled = false;
#if DEBUG
                NavigateToMain(NavPageType.SubjectsPage, "Subjects");
                var s = Subject.Subjects[SubjectType.ELEMENTY_OPTYKI_I_AKUSTYKI];
                NavigateToSubject(s, "EOiA");
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

        public override void ClearDefinitions(Subject subject)
        {
            ViewHandler.DefViews.Clear();
            if (SubjectPages[subject.Base.SubjectType].ContainsKey(NavPageType.DefinitionsPage))
            {
                SubjectPages[subject.Base.SubjectType].Remove(NavPageType.DefinitionsPage);
            }
        }

        public override void ClearFormulas(Subject subject)
        {
            ViewHandler.FmlViews.Clear();
            if (SubjectPages[subject.Base.SubjectType].ContainsKey(NavPageType.FormulasPage))
            {
                SubjectPages[subject.Base.SubjectType].Remove(NavPageType.FormulasPage);
            }
        }

        public override async void NavigateToModal(ContentPage modal)
        {
            await NavPage.PushAsync(modal);
        }

        public override async void NavigateToPage(Page page, bool removePrevious = false)
        {
            if (NavPage.CurrentPage == page)
            {
                NavPage.Title = page.Title;
            }
            else
            {
                if (removePrevious)
                {
                    await NavPage.PopAsync(false);
                }
                await NavPage.PushAsync(page);
            }
        }

        public override void NavigateToMain(NavPageType pageType, string title)
        {
            NavigateTo(pageType, "AiRTech", false);
        }

        public override void NavigateToSubject(Subject subject, string title)
        {
            NavigateTo(NavPageType.SubjectPage, title, true, subject);
        }

        public override void NavigateToDefinition(string title, Subject subject)
        {
            NavigateTo(NavPageType.DefinitionsPage, title, true, subject);
        }

        public override void NavigateToFormulaList(Subject subject, string title)
        {
            NavigateTo(NavPageType.FormulasPage, title, true, subject);
        }

        public override void NavigateToFormula(string name, Subject subject)
        {
            var np = GetPage<FormulasPage>(NavPageType.FormulasPage, subject.Name, subject);
            np?.NavigateToFormula(name);
        }

        public override void NavigateToSolverList(Subject subject, string title)
        {
            var np = GetPage<SolverPage>(NavPageType.SolverPage, subject.Name, subject);
            np?.NavigateToMain();
        }

        public override void NavigateToSolver(Subject subject, string solverName)
        {
            var np = GetPage<SolverPage>(NavPageType.SolverPage, subject.Name, subject);
            var sv = ViewHandler.GetSolverView(subject.Base.SubjectType, solverName);
            if (sv != null)
            {
                np?.NavigateToTab(sv);
            }
            else
            {
                np?.NavigateToMain();
            }
        }

        private async void NavigateTo(NavPageType pageType, string title, bool inner = true, Subject subject = null)
        {
            var mPage = MainPage as MasterDetailPage;
            if (mPage == null)
            {
                return;
            }
            var newPage = GetPage<Page>(pageType, title, subject);
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

        public T GetPage<T>(NavPageType pageType, string title = null, Subject subject = null)
        {
            try
            {
                Page page;
                if (pageType != NavPageType.SubjectPage
                    && pageType != NavPageType.DefinitionsPage
                    && pageType != NavPageType.FormulasPage
                    && pageType != NavPageType.SolverPage)
                {
                    page = GetMainPage(pageType);
                }
                else
                {
                    page = GetPageForSubject(pageType, subject);
                }
                if (!string.IsNullOrWhiteSpace(title) && page.Title != title)
                {
                    page.Title = title;
                }
                if (page != null)
                {
                    return (T)(object)page;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return default(T);
        }

        private Page GetMainPage(NavPageType pageType)
        {
            return !MainPages.ContainsKey(pageType) ? CreatePage(pageType) : MainPages[pageType];
        }

        private T GetPageForSubject<T>(NavPageType pageType, Subject subject)
        {
            var page = GetPageForSubject(pageType, subject);
            if (page != null)
            {
                return (T)Convert.ChangeType(page, typeof(T));
            }
            return default(T);
        }

        private Page GetPageForSubject(NavPageType pageType, Subject subject)
        {
            if (!SubjectPages.ContainsKey(subject.Base.SubjectType)
                || !SubjectPages[subject.Base.SubjectType].ContainsKey(pageType))
            {
                CreatePage(pageType, subject);
            }
            var page = SubjectPages[subject.Base.SubjectType][pageType];
            return page;
        }

        private Page CreatePage(NavPageType pageType, Subject subject = null)
        {
            Page page;
            Type type;
            switch (pageType)
            {
                case NavPageType.MainPage:
                    type = typeof(MainPage);
                    break;
                case NavPageType.AboutPage:
                    type = typeof(AboutPage);
                    break;
                case NavPageType.SubjectsPage:
                    type = typeof(SubjectsPage);
                    break;
                case NavPageType.SubjectPage:
                    type = typeof(SubjectPage);
                    break;
                case NavPageType.DefinitionsPage:
                    type = typeof(DefinitionsPage);
                    break;
                case NavPageType.FormulasPage:
                    type = typeof(FormulasPage);
                    break;
                case NavPageType.SolverPage:
                    type = typeof(SolverPage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
            }
            if (subject == null)
            {
                page = Activator.CreateInstance(type) as Page;
                MainPages.Add(pageType, page);
                return page;
            }
            else
            {
                page = Activator.CreateInstance(type, subject) as Page;
                var subjectType = subject.Base.SubjectType;
                SubjectPages[subjectType].Add(pageType, page);
                return page;
            }
        }

        private NavigationPage NavPage { get; set; }
    }
}
