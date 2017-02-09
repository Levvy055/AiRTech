using System;
using System.Diagnostics;
using AiRTech.Core;
using AiRTech.Core.Subjects;
using AiRTech.Solvers;
using AiRTech.Views.Other;
using AiRTech.Views.Pages;
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
        private readonly Color _topBarTextColor = Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue);
        private MasterDetailPage _mdp;
        private bool _isOnMain;
        private NavPageType _lastType = NavPageType.MainPage;

        public App() : base()
        {
            _mdp = new MasterDetailPage
            {
                Master = new MenuPage
                {
                    BackgroundColor = _menuBgColor,
                    IsBusy = true,
                    IsDisabled = true
                },
                Detail = new ContentPage(),
                MasterBehavior = MasterBehavior.Popover
            };
            MainPage = _mdp;
            try
            {
                DialogManager = new DialogManager();
                DataCore = new CoreManager(this);
                InitSolvers();
                NavigateToMain(NavPageType.MainPage, "AiRTech");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            var menuPage = (MenuPage)((MasterDetailPage)MainPage).Master;
            menuPage.IsBusy = false;
            menuPage.IsDisabled = false;
        }

        protected override void OnStart()
        {
            NavigateToMain(NavPageType.MainPage, "AiRTech");
            try
            {
#if DEBUG
                NavigateToMain(NavPageType.SubjectsPage, "Subjects");
                var s = Subject.Subjects[SubjectType.MECHANIKA];
                NavigateToSubject("Mechana", s);
                NavigateToFormulaList("Mechana", s);
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
            Debug.WriteLine("Sleeping");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("Resuming");
            if (MainPage != _mdp)
            {
                MainPage = _mdp;
            }
            if (_isOnMain)
            {
                NavigateTo(NavPageType.MainPage, "AiR Tech");
            }
            else
            {
                NavigateTo(_lastType);
            }
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

        public override void NavigateBack()
        {
            NavPage.PopAsync();
        }

        public override void NavigateToSubject(string title, Subject subject)
        {
            NavigateTo(NavPageType.SubjectPage, title, true, subject);
        }

        public override void NavigateToDefinition(string name, Subject subject)
        {
            NavigateTo(NavPageType.DefinitionsPage, name, true, subject);
        }

        public override void NavigateToFormulaList(string title, Subject subject)
        {
            NavigateTo(NavPageType.FormulasPage, title, true, subject);
        }

        public override void NavigateToFormula(string name, Subject subject)
        {
            var np = GetPage<FormulasPage>(NavPageType.FormulasPage, subject.Name, subject);
            np?.NavigateToFormula(name);
        }

        public override void NavigateToSolverList(string title, Subject subject)
        {
            var np = GetPage<SolverPage>(NavPageType.SolverPage, subject.Name, subject);
            np?.NavigateToMain();
        }

        public override void NavigateToSolver(string solverName, Subject subject)
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

        public override void NavigateToSearchPage(NavPageType callingPage, Subject subject)
        {
            var title = "Wyszukaj " + (callingPage == NavPageType.DefinitionsPage ? "Definicje" : "Wzory");
            SearchPage.SearchFilter = callingPage;
            NavigateTo(NavPageType.SearchPage, title, true, subject);
        }

        private async void NavigateTo(NavPageType pageType, string title = null, bool inner = true, Subject subject = null)
        {
            var newPage = GetPage<Page>(pageType, title, subject);
            if (newPage != null)
            {
                _isOnMain = pageType == NavPageType.MainPage || pageType == NavPageType.AboutPage;
                _lastType = pageType;
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
                    _mdp.Detail = NavPage;
                }
                _mdp.IsPresented = false;
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
                    && pageType != NavPageType.SolverPage
                    && pageType != NavPageType.SearchPage)
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
                case NavPageType.SearchPage:
                    type = typeof(SearchPage);
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
