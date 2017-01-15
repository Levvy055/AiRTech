using System.Collections.Generic;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Solv;

namespace AiRTech.Core.Subjects
{
    public abstract class SubjectBase
    {
        private SubjectType _subjectType;
        protected SubjectBase(SubjectType subjectType)
        {
            _subjectType = subjectType;
            Definitions=new List<Definition>();
            Solver = Solver.GetSolverFor(_subjectType);
        }

        protected abstract void UpdateDependencies();

        public Solver Solver { get; private set; }
        public List<Definition> Definitions { get; protected set; }
    }
}