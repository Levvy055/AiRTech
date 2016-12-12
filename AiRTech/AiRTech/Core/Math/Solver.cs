using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Core.Math
{
    public abstract class Solver
    {

        public abstract List<SolverView> GetTabs();

        public static Solver GetSolverFor(Type solverType)
        {
            if (ActiveSolvers.ContainsKey(solverType))
            {
                return ActiveSolvers[solverType];
            }
            var solver = (Solver)Activator.CreateInstance(solverType);
            ActiveSolvers.Add(solverType, solver);
            return solver;
        }

        public static Dictionary<Type, Solver> ActiveSolvers { get; } = new Dictionary<Type, Solver>();
    }
}
