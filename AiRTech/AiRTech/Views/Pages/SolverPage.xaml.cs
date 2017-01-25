using System;
using System.Diagnostics;
using System.Windows.Input;
using AiRTech.Core;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Solv;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public partial class SolverPage : ContentPage
    {
        private bool _isOnMain;

        public SolverPage(Subject subject)
        {
            Subject = subject;
            InitSolver();
        }

        private void InitSolver()
        {
            Solver = Subject.Base.Solver;
            var mstack = new StackLayout();
            Carousel = new CarouselPage();
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
                foreach (var tab in solverViews)
                {
                    var sv = new ScrollView
                    {
                        Content = tab,
                        Orientation = ScrollOrientation.Vertical
                    };
                    var page = new ContentPage { Content = sv, Title = tab.Title };
                    var b = new Button
                    {
                        Text = tab.Title,
                        Command = NavigateToTabCommand(page)
                    };
                    mstack.Children.Add(b);
                    Carousel.Children.Add(page);
                }
            }
            Mpage = new ContentPage
            {
                Content = new ScrollView
                {
                    Content = mstack
                },
                Title = "Solver - " + Subject.Name
            };
            Carousel.CurrentPageChanged += CarouselOnPageChanged;
        }

        private void CarouselOnPageChanged(object s, EventArgs eventArgs)
        {
            try
            {
                var t = Carousel.CurrentPage.Title;
                Carousel.Title = t;
                CoreManager.Current.App.NavigateToSolver(Subject, t, true);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void NavigateToMain()
        {
            CoreManager.Current.App.NavigateToPage(Mpage);
            _isOnMain = true;
        }

        private ICommand NavigateToTabCommand(ContentPage page)
        {
            var c = new Command(() =>
            {
                GoToTab(page);
            });
            return c;
        }

        public void NavigateToTab(View v)
        {
            foreach (var page in Carousel.Children)
            {
                var scroll = page.Content as ScrollView;
                if (scroll?.Content == v)
                {
                    GoToTab(page);
                }
            }
        }

        private void GoToTab(ContentPage page)
        {
            Carousel.CurrentPage = page;
            Carousel.Title = page.Title + " - Solver";
            CoreManager.Current.App.NavigateToPage(Carousel, _isOnMain);
            _isOnMain = false;
        }

        public Subject Subject { get; set; }
        public Solver Solver { get; private set; }
        public ContentPage Mpage { get; private set; }
        public CarouselPage Carousel { get; set; }
    }
}
