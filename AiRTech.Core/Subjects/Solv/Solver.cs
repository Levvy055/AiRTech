using System;
using System.Collections.Generic;
using System.Linq;

namespace AiRTech.Core.Subjects.Solv
{
    public abstract class Solver
    {
        protected Dictionary<string, SolverView> _tabs;
        private static readonly Dictionary<SubjectType, Solver> _solvers = new Dictionary<SubjectType, Solver>();

        protected Solver(SubjectType subjectType)
        {
            SubjectType = subjectType;
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

        public abstract void InitSolverTabs(Action<Solver> sa);

        private static Dictionary<SubjectType, Solver> Solvers => _solvers;
        public SubjectType SubjectType { get; private set; }

        public IEnumerable<SolverView> Tabs => _tabs.Values.ToList();
    }
}
