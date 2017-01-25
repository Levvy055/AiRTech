using System.Collections.Generic;
using AiRTech.Views.ViewComponents;

namespace AiRTech.Core.Subjects.Solv.Solvers
{
    public class ElectronicBasicsSolver : Solver
    {
        public ElectronicBasicsSolver() : base(SubjectType.PODSTAWY_ELEKTRONIKI)
        {
        }

        public override Dictionary<string, SolverView> InitSolverTabs()
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
