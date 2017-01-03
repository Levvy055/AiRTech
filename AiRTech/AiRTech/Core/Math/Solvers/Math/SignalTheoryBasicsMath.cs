using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Math.Solvers.Components;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Math
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
        public static IOrderedEnumerable<KeyValuePair<Tuple<double, double>, double>> GetHistResults(Entry[,] e1, Entry[,] e2)
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
            var en=d.OrderBy(pair => pair.Key.Item1).ThenBy(pair => pair.Key.Item2);
            return en;
        }

        private static double[,] GetNumbersFromEntries(Entry[,] e)
        {
            var sx = e.GetLength(0);
            var sy = e.GetLength(1);
            var n = new double[sx, sy];
            for (var i = 0; i < sx; i++)
            {
                for (var j = 0; j < sy; j++)
                {
                    var vs = e[i, j]?.Text;
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
    }
}
