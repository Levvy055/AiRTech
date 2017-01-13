using System.Collections.Generic;
using AiRTech.Views.ViewComponents;

namespace AiRTech.Core.Math.Solvers
{
    public class ElectronicBasicsSolver : Solver
    {
        public override Dictionary<string, SolverView> Tabs
        {
            get
            {
                var list = new Dictionary<string, SolverView>
            {
                {"First", new SolverView(null)}
            };
                return list;
            }
        }
    }
}
