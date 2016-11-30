using AiRTech.Core.Math;
using AiRTech.Core.Math.Solvers;

namespace AiRTech.Core.Subjects
{
    public class ElectronicBasics : SubjectBase
    {
        public ElectronicBasics() : base()
        {

        }

        protected override void CreateDependencies()
        {
            Solver = Solver.GetSolverFor(typeof(ElectronicBasicsSolver));
        }
    }
}