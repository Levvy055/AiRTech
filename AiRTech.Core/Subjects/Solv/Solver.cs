using System;
using System.Collections.Generic;
using AiRTech.Views.ViewComponents;

namespace AiRTech.Core.Subjects.Solv
{
    public abstract class Solver
    {
        protected Dictionary<string, SolverView> _tabs;
        private static readonly Dictionary<SubjectType, Solver> _solvers = new Dictionary<SubjectType, Solver>();

        protected Solver(SubjectType subjectType)
        {
            Solvers.Add(subjectType, this);
        }

        public static Solver GetSolverFor(SubjectType subjectType)
        {
            if (Solvers.ContainsKey(subjectType))
            {
                return Solvers[subjectType];
            }
            return null;
        }

        public abstract Dictionary<string, SolverView> InitSolverTabs();

        private static Dictionary<SubjectType, Solver> Solvers => _solvers;
    }
}
