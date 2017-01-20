using System;
using System.Diagnostics;
using System.Windows.Input;
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
            var tabs = Solver.Tabs;
            Carousel = new CarouselPage();
            var mstack = new StackLayout();
            if (tabs != null && tabs.Count != 0)
            {
                foreach (var tab in tabs)
                {
                    var sv = new ScrollView
                    {
                        Content = tab.Value,
                        Orientation = ScrollOrientation.Vertical
                    };
                    var page = new ContentPage { Content = sv, Title = tab.Key };
                    var b = new Button
                    {
                        Text = tab.Key,
                        Command = NavigateToTabCommand(page)
                    };
                    mstack.Children.Add(b);
                    Carousel.Children.Add(page);
                }
            }
            else
            {
                mstack.Children.Add(new Label
                {
                    Text = "Solver not yet implemented!"
                });
            }
            Mpage = new ContentPage { Content = new ScrollView { Content = mstack }, Title = "Solver - " + Subject.Name };
            Carousel.CurrentPageChanged += CarouselOnPageChanged;
        }

        private void CarouselOnPageChanged(object s, EventArgs eventArgs)
        {
            try
            {
                var t = Carousel.CurrentPage.Title;
                Carousel.Title = t;
                var app = Application.Current as App;
                app.NavigateTo(Carousel);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void NavigateToMain()
        {
            var app = Application.Current as App;
            app.NavigateTo(Mpage);
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
            var app = Application.Current as App;
            app.NavigateTo(Carousel, _isOnMain);
            _isOnMain = false;
        }

        public Subject Subject { get; set; }
        public Solver Solver { get; private set; }
        public ContentPage Mpage { get; private set; }
        public CarouselPage Carousel { get; set; }
    }
}
