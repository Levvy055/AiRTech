using AiRTech.Core.Math;

namespace AiRTech.Core.Subjects
{
    public abstract class SubjectBase
    {
        protected SubjectBase()
        {
            CreateDependencies();
        }

        protected abstract void CreateDependencies();

        public Solver Solver { get; protected set; }
    }
}