using System;
using System.Collections.Generic;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Solv;

namespace AiRTech.Solvers
{
    public class ElectronicBasicsSolver : Solver
    {
        public ElectronicBasicsSolver() : base(SubjectType.PODSTAWY_ELEKTRONIKI)
        {
        }

        public override void InitSolverTabs(Action<Solver> sa)
        {
            if (_tabs != null)
            {
                return;
            }
            _tabs = new Dictionary<string, SolverView>
            {
                //{"First", new SolverView(null)}
            };
            foreach (var tabPair in _tabs)
            {
                tabPair.Value.Title = tabPair.Key;
            }
            sa.Invoke(this);
        }
    }
}
