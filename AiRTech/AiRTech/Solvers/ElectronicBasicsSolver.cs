using System.Collections.Generic;
using AiRTech.Views.ViewComponents;

namespace AiRTech.Core.Subjects.Solv.Solvers
{
    public class ElectronicBasicsSolver : Solver
    {
        public override Dictionary<string, SolverView> SolverTabs
        {
            get
            {
                if (_tabs != null)
                {
                    return _tabs;
                }
                var list = new Dictionary<string, SolverView>
            {
                //{"First", new SolverView(null)}
            };
                return list;
            }
        }
    }
}
