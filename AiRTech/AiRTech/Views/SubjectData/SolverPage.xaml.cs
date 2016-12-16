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
            BarBackgroundColor = Color.Teal;
            BarTextColor = Color.White;
            Subject = subject;
            InitSolver();
        }

        private void InitSolver()
        {
            Solver = Subject.Base.Solver;
            var tabs = Solver.GetTabs();
            if (tabs == null || tabs.Count == 0)
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
            else
            {
                foreach (var tab in tabs)
                {
                    var page = new ContentPage { Content = tab.Value, Title = tab.Key };
                    Children.Add(page);
                }
            }
        }

        public Subject Subject { get; set; }
        public Solver Solver { get; private set; }
    }
}
