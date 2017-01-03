using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void OnConvertLeft(object source, EventArgs args)
        {
            Debug.WriteLine("To Left Conv");
        }

        private void OnConvertRight(object source, EventArgs args)
        {
            Debug.WriteLine("To Right Conv");
        }

        private SolverView DecibelsView => new SolverView
        {
            Contento = new ViewComponent[,]
            {
                { new SvRow(new SvLabel("k = "), new SvTxtField("k"), new SvTxtField("P_o"), new SvLabel(" = P_o")) },
                { new SvRow(new SvLabel("A = "),new SvTxtField("A"), new SvButton("", OnConvertLeft, "arrow_left.png"), new SvButton("", OnConvertRight,"arrow_right.png"), new SvTxtField("P"), new SvLabel(" = P")) }
            }
        };

        private SolverView HistogramView => new SolverView
        {
            Contento = new ViewComponent[,] {
                { new SvLabel("Row, Column count"),  new SvTxtField("k") }
            }
        };
    }

}
