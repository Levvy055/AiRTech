using AiRTech.Core.Math;

namespace AiRTech.Core.Subjects
{
    public abstract class SubjectBase
    {
        private SubjectType _subjectType;
        protected SubjectBase(SubjectType subjectType)
        {
            _subjectType = subjectType;
            Solver = Solver.GetSolverFor(_subjectType);
        }

        protected abstract void UpdateDependencies();

        public Solver Solver { get; private set; }
    }
}