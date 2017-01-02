using AiRTech.Core.Math;
using AiRTech.Core.Math.Solvers;

namespace AiRTech.Core.Subjects
{
    public class SignalTheoryBasics : SubjectBase
    {
        public SignalTheoryBasics() : base(SubjectType.PODSTAWY_TEORII_SYGNALOW)
        {
        }

        protected override void UpdateDependencies()
        {
            Solver = Solver.GetSolverFor(SubjectType.PODSTAWY_TEORII_SYGNALOW);
        }
    }
}