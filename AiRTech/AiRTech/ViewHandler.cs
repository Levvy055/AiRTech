using System;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Solv;
using AiRTech.Core.Subjects.Solv.Solvers;

namespace AiRTech
{
    public static class ViewHandler
    {
        public static Solver GetSolverFor(SubjectType subjectType)
        {
            if (Solver.Solvers.ContainsKey(subjectType))
            {
                return Solver.Solvers[subjectType];
            }
            return null;
        }
    }
}
