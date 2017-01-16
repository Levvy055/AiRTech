using System;
using System.Collections.Generic;
using System.Linq;
using AiRTech.Core.Subjects.Solv.Solvers.Components;
using AiRTech.Core.Subjects.Solv.Solvers.Math.Utils;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Solv.Solvers.Math
{
    public static class SignalTheoryBasicsMath
    {
        #region Decibels
        public static double CalcDb(Dictionary<string, ViewComponent> uc, bool inverted)
        {
            var kS = uc["db_k"].GetSourceAs<Entry>().Text;
            var poS = uc["db_po"].GetSourceAs<Entry>().Text;
            var aS = uc["db_a"].GetSourceAs<Entry>().Text;
            var pS = uc["db_p"].GetSourceAs<Entry>().Text;
            int k, a, p, po;
            if (int.TryParse(kS, out k)
                && (int.TryParse(aS, out a) || inverted)
                && (int.TryParse(pS, out p) || !inverted)
                && int.TryParse(poS, out po))
            {
                return CalcDb(a, p, k, po, inverted);
            }
            throw new ArgumentException("Wrong params: {" + $"k: {kS}, a: {aS}, p: {pS}, P_o: {poS}" + "}");
        }

        private static double CalcDb(double a, double p, double k, double po, bool inverted)
        {
            double res;
            if (inverted)
            {
                res = k * System.Math.Log10(p / po);
            }
            else
            {
                res = System.Math.Pow(10, a / k) * po;
            }
            return res;
        }
        #endregion

        #region Histogram
        public static IOrderedEnumerable<KeyValuePair<Tuple<double, double>, double>> GetHistResults(View[,] e1, View[,] e2)
        {
            var h1 = GetNumbersFromEntries(e1);
            var h2 = GetNumbersFromEntries(e2);
            var sx = h1.GetLength(0);
            var sy = h1.GetLength(1);
            var d = new Dictionary<Tuple<double, double>, double>();
            for (var i = 0; i < sx; i++)
            {
                for (var j = 0; j < sy; j++)
                {
                    var v1 = h1[i, j];
                    var v2 = h2[i, j];
                    var t = Tuple.Create(v2, v1);
                    if (!d.ContainsKey(t))
                    {
                        d.Add(t, 1);
                    }
                    else
                    {
                        d[t]++;
                    }
                }
            }
            var en = d.OrderBy(pair => pair.Key.Item1).ThenBy(pair => pair.Key.Item2);
            return en;
        }

        private static double[,] GetNumbersFromEntries(View[,] es)
        {
            var sx = es.GetLength(0);
            var sy = es.GetLength(1);
            var n = new double[sx, sy];
            for (var i = 0; i < sx; i++)
            {
                for (var j = 0; j < sy; j++)
                {
                    var e = es[i, j] as Entry;
                    var vs = e?.Text;
                    if (vs == null) { continue; }
                    double v;
                    if (double.TryParse(vs, out v))
                    {
                        n[i, j] = v;
                    }
                }
            }
            return n;
        }

        #endregion

        #region Signal Analysis

        public static void AnalyzeSignal(List<double> samples, ref Dictionary<SignalDataType, Entry> entries)
        {
            var core = new SignalAnalysisCore(samples);
            core.Calc();
            foreach (var entry in entries)
            {
                entry.Value.Text = core.GetOutput(entry.Key);
            }
        }

        public enum SignalDataType
        {
            // ReSharper disable InconsistentNaming
            DIRECT_CURRENT, ALTERNATE_CURRENT, ENERGY_ALL, ENERGY_DC, ENERGY_AC,
            ENERGY_Xi, POWER_AVG, POWER_DC, POWER_AC, RMS, BIAS_AVG, BIAS_STD
            // ReSharper restore InconsistentNaming
        }

        public class SignalData
        {
            public SignalData(double value)
            {
                Value = value;
                IsArray = false;
            }

            public SignalData(double[] values)
            {
                Values = values;
                IsArray = true;
            }

            public double Value { get; }

            public double[] Values { get; }

            public bool IsArray { get; private set; }
        }

        private class SignalAnalysisCore
        {
            public SignalAnalysisCore(List<double> samples)
            {
                Samples = samples;
                Outputs = new Dictionary<SignalDataType, SignalData>();
            }

            public void Calc()
            {
                AddData(SignalDataType.DIRECT_CURRENT, CalcDc());
                AddData(SignalDataType.ALTERNATE_CURRENT, CalcAc());
                AddData(SignalDataType.ENERGY_Xi, CalcEi());
                AddData(SignalDataType.ENERGY_ALL, CalcEc());
                AddData(SignalDataType.ENERGY_AC, CalcEac());
                AddData(SignalDataType.ENERGY_DC, CalcEdc());
                AddData(SignalDataType.POWER_AVG, CalcPAvg());
                AddData(SignalDataType.POWER_AC, CalcPac());
                AddData(SignalDataType.POWER_DC, CalcPdc());
                AddData(SignalDataType.RMS, CalcRms());
                AddData(SignalDataType.BIAS_STD, CalcBstd());
            }

            private void AddData(SignalDataType signalDataType, double value)
            {
                Outputs.Add(signalDataType, new SignalData(value));
            }

            private void AddData(SignalDataType signalDataType, double[] values)
            {
                Outputs.Add(signalDataType, new SignalData(values));
            }

            #region Signal Analysis Calculations
            private double CalcDc()
            {
                var sum = Samples.Sum();
                return sum / N;
            }

            private double[] CalcAc()
            {
                var acArray = new double[N];
                var X_DC = Outputs[SignalDataType.DIRECT_CURRENT].Value;
                for (var i = 0; i < N; i++)
                {
                    var X_i = Samples[i];
                    acArray[i] = X_i - X_DC;
                }
                return acArray;
            }

            private double[] CalcEi()
            {
                var E_i = new double[N];
                for (var i = 0; i < N; i++)
                {
                    var X_i = Samples[i];
                    E_i[i] = System.Math.Pow(System.Math.Abs(X_i), 2);
                }
                return E_i;
            }

            private double CalcEc()
            {
                var values = Outputs[SignalDataType.ENERGY_Xi].Values;
                double sum = 0;
                for (var i = 0; i < N; i++)
                {
                    sum += values[i];
                }
                return sum;
            }

            private double CalcEdc()
            {
                var value = Outputs[SignalDataType.DIRECT_CURRENT].Value;
                return System.Math.Pow(value, 2) * N;
            }

            private double CalcEac()
            {
                var values = Outputs[SignalDataType.ALTERNATE_CURRENT].Values;
                double sum = 0;
                for (var i = 0; i < N; i++)
                {
                    sum += System.Math.Pow(values[i], 2);
                }
                return sum;
            }

            private double CalcPAvg()
            {
                var E = Outputs[SignalDataType.ENERGY_ALL].Value;
                return E / N;
            }

            private double CalcPdc()
            {
                return System.Math.Pow(Outputs[SignalDataType.DIRECT_CURRENT].Value, 2);
            }

            private double CalcPac()
            {
                var values = Outputs[SignalDataType.ALTERNATE_CURRENT].Values;
                double res = 0;
                for (var i = 0; i < N; i++)
                {
                    res += System.Math.Pow(values[i], 2);
                }
                return res / N;
            }

            private double CalcRms()
            {
                return System.Math.Sqrt(Outputs[SignalDataType.POWER_AVG].Value);
            }

            private double CalcBstd()
            {
                double res = 0;
                var values = Outputs[SignalDataType.ALTERNATE_CURRENT].Values;
                for (var i = 0; i < N; i++)
                {
                    res += System.Math.Pow(System.Math.Abs(values[i]), 2);
                }
                return System.Math.Sqrt(res / (N - 1));
            }
            #endregion

            public string GetOutput(SignalDataType signalDataType)
            {
                if (!Outputs.ContainsKey(signalDataType))
                {
                    throw new NullReferenceException("Key not exists");
                }
                var vs = Outputs[signalDataType];
                if (!vs.IsArray)
                {
                    return vs.Value.ToString();
                }
                var s = "[";
                for (var i = 0; i < vs.Values.Length; i++)
                {
                    var v = vs.Values[i];
                    s += v + (i + 1 != vs.Values.Length ? "; " : "]");
                }
                return s;
            }

            private List<double> Samples { get; }
            private int N => Samples.Count;
            private Dictionary<SignalDataType, SignalData> Outputs { get; }
        }
        #endregion

        #region Signal in Signal
        public static bool SisFrom(string vs, string ws, bool vToW, out bool ortogonal, out bool ortonormal, out double p, out SimpleVector<double> pV, out double ev, out double ew)
        {
            SimpleVector<double> v1, w1;
            if (VectorUtils.TryParseToVector(vs.Trim(), out v1) && VectorUtils.TryParseToVector(ws.Trim(), out w1) && v1.Size == w1.Size)
            {
                ev = v1.Sum(vl => System.Math.Pow(vl, 2));
                ew = w1.Sum(wl => System.Math.Pow(wl, 2));
                var v = vToW ? v1 : w1;
                var w = !vToW ? v1 : w1;
                var vMw = System.Math.Abs(v * w);
                var vMv = System.Math.Abs(v * v);
                ortogonal = vMw <= 0;
                ortonormal = ortogonal && (System.Math.Abs(vMv - 1) <= 0 && System.Math.Abs(w * w - 1) <= 0);
                p = vMw / vMv;
                p *= 100;
                p = System.Math.Round(p, 2);
                p /= 100;
                pV = p * v;
                pV.Round(2);
                return true;
            }
            p = 0;
            pV = null;
            ev = 0;
            ew = 0;
            ortogonal = false;
            ortonormal = false;
            return false;
        }


        #endregion
    }
}
