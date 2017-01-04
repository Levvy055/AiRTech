using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AiRTech.Core.Math.Solvers.Components;
using AiRTech.Core.Math.Solvers.Math;
using AiRTech.Views.Other;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers
{
    public class SignalTheoryBasicsSolver : Solver
    {
        #region view Fields
        private SolverView _decView;
        private SolverView _histView;
        private SolverView _signalView;
        #endregion

        #region Main
        public override Dictionary<string, SolverView> GetTabs()
        {
            var list = new Dictionary<string, SolverView>
            {
                {"Decibels", DecibelsView},
                {"Histogram", HistogramView},
                {"Signal Analysis", SignalView},
                {"Harmoniczne", new SolverView(null)},
                {"DFT", new SolverView(null)},
                {"FFT", new SolverView(null)},
                {"A-law", new SolverView(null)},
                {"M-law", new SolverView(null)},
                {"Graphs", new SolverView(null)}
            };
            return list;
        }

        private Dictionary<string, ViewComponent> Uc { get; } = new Dictionary<string, ViewComponent>();
        #endregion

        #region Event Handlers
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


        private void OnCreateHist(object o, EventArgs eventArgs)
        {
            try
            {
                var sizeS = Uc["h_s"].GetSourceAs<Entry>().Text;
                int size;
                if (string.IsNullOrWhiteSpace(sizeS) || !int.TryParse(sizeS, out size))
                {
                    throw new ArgumentException("Not a number or empty!");
                }
                Debug.WriteLine("Size: " + size);
                var gl = Uc["h_l"] as SvGrid;
                var gr = Uc["h_r"] as SvGrid;
                if (gl == null || gr == null)
                {
                    throw new NullReferenceException("h_l or h_r not found!");
                }
                gl.ResetGridToSqEntry(size);
                gr.ResetGridToSqEntry(size);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void OnHistCalc(object o, EventArgs eventArgs)
        {
            try
            {
                var gl = Uc["h_l"] as SvGrid;
                var gr = Uc["h_r"] as SvGrid;
                if (gl == null || gr == null)
                {
                    throw new NullReferenceException("h_l or h_r not found!");
                }
                var r = SignalTheoryBasicsMath.GetHistResults(gl.Entries, gr.Entries);
                var modal = new HistResultModal(r.ToList());
                modal.Show();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void OnSaProbesCountChange(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            try
            {
                var tf = sender as Entry;
                if (tf == null)
                {
                    throw new ArgumentException("Sender is null or not Entry type!");
                }
                var txt = tf.Text;
                int count;
                if (string.IsNullOrWhiteSpace(txt) || !int.TryParse(txt, out count))
                {
                    throw new ArgumentException(txt + " is not a valid integer number!");
                }
                var pg = Uc["sa_probes"].Source as Grid;
                pg.Children.Clear();
                for (var i = 0; i < count; i++)
                {
                    pg.Children.Add(new Entry(), i, 0);
                    if (i >= 16)
                    {
                        throw new ArgumentException("Too large!");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion

        #region view Properties
        private SolverView DecibelsView
        {
            get
            {
                if (_decView != null)
                {
                    return _decView;
                }
                var tfK = new SvTxtField("db_k", Uc, "k");
                var tfA = new SvTxtField("db_a", Uc, "A");
                var tfP = new SvTxtField("db_p", Uc, "P");
                var tfPo = new SvTxtField("db_po", Uc, "P_o");
                _decView = new SolverView(new ViewComponent[,]
                    {
                        {new SvLabel("A = k*log_10(P/P_o)")},
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
                    });
                return _decView;
            }
        }

        private SolverView HistogramView
        {
            get
            {
                if (_histView != null)
                {
                    return _histView;
                }
                var tfS = new SvTxtField("h_s", Uc, "Size");
                var gl = new SvGrid("h_l", Uc);
                var gr = new SvGrid("h_r", Uc);
                _histView = new SolverView(new ViewComponent[,]
                    {
                        {new SvLabel("Write Row & Column Count (max 20):"), tfS, new SvButton("Create table", OnCreateHist), },
                        {new SvRow(gl), new SvButton("Calc", OnHistCalc), new SvRow(gr) }
                    });
                return _histView;
            }
        }

        public SolverView SignalView
        {
            get
            {
                if (_signalView != null)
                {
                    return _signalView;
                }
                var tfProbes = new SvTxtField("sa_probes_count", Uc, "(max 16)");
                var tfp = tfProbes.GetSourceAs<Entry>();
                tfp.WidthRequest = 10;
                tfp.TextChanged += OnSaProbesCountChange;
                var gridProbes = new SvGrid("sa_probes", Uc);
                var gridResults = new SvGrid("sa_results", Uc);
                _signalView = new SolverView(new ViewComponent[,]
                {
                    {new SvRow(
                        new SvLabel("Ilość próbek sygnału"),
                        tfProbes)
                    { ColumnsRatio = new []{ 2d, 10d } }  },
                    {new SvRow(
                        new SvLabel("Próbki: "),
                        gridProbes)
                    { ColumnsRatio = new []{ 2d, 10d } }  },
                    {new SvRow(
                        gridResults)}
                });
                return _signalView;
            }
        }

        #endregion
    }
}
