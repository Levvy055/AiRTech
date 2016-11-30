using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Math;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class SolverPage : ContentPage
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
        }

        public Subject Subject { get; set; }
        public Solver Solver { get; private set; }
    }
}
