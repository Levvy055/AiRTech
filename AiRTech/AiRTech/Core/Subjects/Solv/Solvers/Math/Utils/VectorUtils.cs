using System.Collections.Generic;

namespace AiRTech.Core.Subjects.Solv.Solvers.Math.Utils
{
    public static class VectorUtils
    {

        public static bool TryParseToVector(string vs, out SimpleVector<double> vector)
        {
            if (!string.IsNullOrWhiteSpace(vs))
            {
                var vValues = vs.Split(' ');
                if (vValues.Length > 0)
                {
                    var vec = new List<double>();
                    foreach (var val in vValues)
                    {
                        if (!string.IsNullOrWhiteSpace(val))
                        {
                            double v;
                            if (double.TryParse(val, out v))
                            {
                                vec.Add(v);
                            }
                        }
                    }
                    if (vec.Count > 0)
                    {
                        vector = new SimpleVector<double>(vec);
                        return true;
                    }
                }
            }
            vector = null;
            return false;
        }
    }
}
