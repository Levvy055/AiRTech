using System.Collections.Generic;
using AiRTech.Core.Math;
using AiRTech.Core.Subjects;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public partial class SolverPage : ExtendedTabbedPage
    {

        public SolverPage(Subject subject)
        {
            TintColor = Color.FromHex("#00806E");
            BarTintColor = Color.FromHex("#00806E");
            SwipeEnabled = true;
            Subject = subject;
            InitSolver();
        }

        private void InitSolver()
        {
            Solver = Subject.Base.Solver;
            var tabs = Solver.GetTabs();
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
                    Children.Add(page);
                }
            }
            else
            {
                Children.Add(new ContentPage
                {
                    Title = "Lack of Pages",
                    Content = new Label
                    {
                        Text = "Not yet implemented! Come back later."
                    }
                });
            }
        }

        public void NavigateTo(int i)
        {
            if (Children.Count < i)
            {
                CurrentPage = Children[i];
            }
        }

        public Subject Subject { get; set; }
        public Solver Solver { get; private set; }
    }
}
