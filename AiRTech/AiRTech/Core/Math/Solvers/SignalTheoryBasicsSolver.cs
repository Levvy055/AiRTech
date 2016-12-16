using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers
{
    public class SignalTheoryBasicsSolver : Solver
    {
        public override Dictionary<string, SolverView> GetTabs()
        {
            var list = new Dictionary<string, SolverView>
            {
                {"Decibels", DecibelsView},
                {"Histogram", HistogramView},
                {"Signal Analysis", new SolverView()},
                {"Harmoniczne", new SolverView()},
                {"DFT", new SolverView()},
                {"FFT", new SolverView()},
                {"A-law", new SolverView()},
                {"M-law", new SolverView()},
                {"Graphs", new SolverView()}
            };
            return list;
        }

        private static SolverView DecibelsView => new SolverView
        {
            Contento = new View[,] {
                { null,new Entry {Placeholder = "k"}, new Entry {Placeholder = "P_o"},null },
                { new Entry {Placeholder = "A"}, new Button {Text = "<-"}, new Button {Text = "->"}, new Entry {Placeholder = "P"} }
            }
        };

        private static SolverView HistogramView => new SolverView
        {
            Contento = new View[,] {
                { new Label {Text = "Row, Column count"},  new Entry {Placeholder = "k"} }
            }
        };
    }

}
