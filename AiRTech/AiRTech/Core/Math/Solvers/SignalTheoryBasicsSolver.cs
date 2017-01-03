using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Math.Solvers.Components;
using AiRTech.Core.Math.Solvers.Math;
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

        private Dictionary<string, ViewComponent> Uc { get; } = new Dictionary<string, ViewComponent>();

        private void OnConvertLeft(object source, EventArgs args)
        {
            try
            {
                Debug.WriteLine("To Left Conv");
                var r = SignalTheoryBasicsMath.CalcDb(Uc, true).ToString();
                Uc["db_a"].GetSourceAs<Entry>().Text = r;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void OnConvertRight(object source, EventArgs args)
        {
            try
            {
                Debug.WriteLine("To Right Conv");
                var r = SignalTheoryBasicsMath.CalcDb(Uc, false).ToString();
                Uc["db_p"].GetSourceAs<Entry>().Text = r;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }



        private SolverView DecibelsView
        {
            get
            {
                var tfK = new SvTxtField("db_k", Uc, "k");
                var tfA = new SvTxtField("db_a", Uc, "A");
                var tfP = new SvTxtField("db_p", Uc, "P");
                var tfPo = new SvTxtField("db_po", Uc, "P_o");
                return new SolverView
                {
                    Contento = new ViewComponent[,]
                    {   {new SvLabel("A = k*log_10(P/P_o)") },
                        {
                            new SvRow(
                                new SvLabel("k = "),
                                tfK,
                                tfPo,
                                new SvLabel(" = P_o"))
                        },
                        {
                            new SvRow(
                                new SvLabel("A = "),
                                tfA,
                                new SvButton("", OnConvertLeft, "arrow_left.png"),
                                new SvButton("", OnConvertRight, "arrow_right.png"),
                                tfP,
                                new SvLabel(" = P"))
                        }
                    }
                };
            }
        }

        private SolverView HistogramView => new SolverView
        {
            Contento = new ViewComponent[,] {
                { new SvLabel("Row, Column count"),  new SvTxtField("h_k",Uc,"k") }
            }
        };
    }

}
