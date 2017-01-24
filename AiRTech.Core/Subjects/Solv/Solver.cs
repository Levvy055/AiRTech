using System;
using System.Collections.Generic;
using AiRTech.Views.ViewComponents;

namespace AiRTech.Core.Subjects.Solv
{
    public abstract class Solver
    {
        protected Dictionary<string, SolverView> _tabs;

        protected Solver(SubjectType subjectType)
        {
            Solvers.Add(subjectType, this);
        }

        public abstract Dictionary<string, SolverView> SolverTabs { get; }

        public static Dictionary<SubjectType, Solver> Solvers { get; } = new Dictionary<SubjectType, Solver>();
    }
}
