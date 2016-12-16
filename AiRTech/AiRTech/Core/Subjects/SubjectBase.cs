using AiRTech.Core.Math;

namespace AiRTech.Core.Subjects
{
    public abstract class SubjectBase
    {
        private SubjectType _subjectType;
        protected SubjectBase(SubjectType subjectType)
        {
            _subjectType = subjectType;
            CreateDependencies();
        }

        protected abstract void CreateDependencies();

        public Solver Solver { get; protected set; }
    }
}