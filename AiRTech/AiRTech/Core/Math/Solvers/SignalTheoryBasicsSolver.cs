using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Math.Solvers.Components;
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
            Contento = new ViewComponent[,]
            {
                {new SvRow(null, new SvTxtField("k"), new SvTxtField("P_o"), null)},
                {new SvRow(new SvTxtField("A"), new SvButton("<-"), new SvButton("->"), new SvTxtField("P"))}
            }
        };

        private static SolverView HistogramView => new SolverView
        {
            Contento = new ViewComponent[,] {
                { new SvLabel("Row, Column count"),  new SvTxtField("k") }
            }
        };
    }

}
