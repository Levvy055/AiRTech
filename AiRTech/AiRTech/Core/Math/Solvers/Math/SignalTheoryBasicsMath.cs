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
        public static double CalcDb(Dictionary<string, ViewComponent> uc, bool inverted)
        {
            var kS = uc["db_k"].GetSourceAs<Entry>().Text;
            var poS = uc["db_po"].GetSourceAs<Entry>().Text;
            var aS = uc["db_a"].GetSourceAs<Entry>().Text;
            var pS = uc["db_p"].GetSourceAs<Entry>().Text;
            int k, a, p, po;
            if (int.TryParse(kS, out k) && (int.TryParse(aS, out a) || inverted)
                && (int.TryParse(pS, out p) || !inverted) && int.TryParse(poS, out po))
            {
                Debug.WriteLine("{" + $"k: {kS}, a: {aS}, p: {pS}, P_o: {poS}" + "}");
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
    }
}
