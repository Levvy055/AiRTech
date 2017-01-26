using System;
using System.Diagnostics;
using System.Windows.Input;
using AiRTech.Core;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Solv;
using Xamarin.Forms;

namespace AiRTech.Views.Pages
{
    public partial class SolverPage : ContentPage
    {
        private bool _isOnMain;

        public SolverPage(Subject subject)
        {
            Subject = subject;
            BindingContext = Subject;
            InitializeComponent();
            InitSolver();
        }

        private void InitSolver()
        {
            Solver = Subject.Base.Solver;
            var mstack = new StackLayout();
            Carousel = new CarouselPage();
            _isOnMain = true;
            var solverViews = ViewHandler.GetSolverViews(Subject.Base.SubjectType);
            if (Solver == null || solverViews == null || solverViews.Count == 0)
            {
                mstack.Children.Add(new Label
                {
                    Text = "Solver not yet implemented!"
                });
            }
            else
            {
                foreach (var solverView in solverViews)
                {
                    var sv = new ScrollView
                    {
                        Content = solverView,
                        Orientation = ScrollOrientation.Vertical
                    };
                    var tab = new ContentPage { Content = sv, Title = solverView.Title };
                    var btn = new Button
                    {
                        Text = solverView.Title,
                        Command = NavigateToTabCommand(tab)
                    };
                    mstack.Children.Add(btn);
                    Carousel.Children.Add(tab);
                }
            }

            Sv.Content = mstack;
            Title = "Solver - " + Subject.Name;
            Carousel.CurrentPageChanged += CarouselOnPageChanged;
        }

        private void CarouselOnPageChanged(object s, EventArgs eventArgs)
        {
            try
            {
                var t = Carousel.CurrentPage.Title;
                Carousel.Title = t;
                var sv = ViewHandler.GetSolverView(Subject.Base.SubjectType, t);
                NavigateToTab(sv);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private ICommand NavigateToTabCommand(ContentPage page)
        {
            var c = new Command(() =>
            {
                GoToTab(page);
            });
            return c;
        }

        public void NavigateToMain()
        {
            CoreManager.Current.App.NavigateToPage(this,!_isOnMain);
            _isOnMain = true;
        }

        public void NavigateToTab(SolverView v)
        {
            if (v == null)
            {
                return;
            }
            foreach (var page in Carousel.Children)
            {
                var scroll = page.Content as ScrollView;
                if (scroll?.Content == v)
                {
                    GoToTab(page);
                    break;
                }
            }
        }

        private void GoToTab(ContentPage page)
        {
            Carousel.CurrentPage = page;
            Carousel.Title = page.Title + " - Solver";
            CoreManager.Current.App.NavigateToPage(Carousel, !_isOnMain);
            _isOnMain = false;
        }

        public Subject Subject { get; set; }
        public Solver Solver { get; private set; }
        public CarouselPage Carousel { get; set; }
    }
}
