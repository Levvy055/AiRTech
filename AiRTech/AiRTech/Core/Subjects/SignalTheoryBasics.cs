using AiRTech.Core.Math;
using AiRTech.Core.Math.Solvers;

namespace AiRTech.Core.Subjects
{
    public class SignalTheoryBasics : SubjectBase
    {
        public SignalTheoryBasics(SubjectType subjectType) : base(subjectType)
        {
        }

        protected override void CreateDependencies()
        {
            Solver = Solver.GetSolverFor(typeof(SignalTheoryBasicsSolver), SubjectType.PODSTAWY_TEORII_SYGNALOW);
        }
    }
}