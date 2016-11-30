using AiRTech.Core.Math;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public partial class SolverPage : TabbedPage
    {

        public SolverPage(Subject subject)
        {
            Subject = subject;
            InitializeComponent();
            InitSolver();
        }

        private void InitSolver()
        {
            Solver = Subject.Base.Solver;
            foreach (var page in Solver.GetView().Children)
            {
                Children.Add(page);
            }
        }

        public Subject Subject { get; set; }
        public Solver Solver { get; private set; }
    }
}
