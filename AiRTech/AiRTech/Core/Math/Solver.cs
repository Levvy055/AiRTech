using System;
using System.Collections.Generic;
using AiRTech.Core.Math.Solvers;
using AiRTech.Core.Subjects;
using AiRTech.Views.ViewComponents;

namespace AiRTech.Core.Math
{
    public abstract class Solver
    {

        public abstract Dictionary<string, SolverView> Tabs { get; }

        public static Solver GetSolverFor(SubjectType subjectType)
        {
            if (ActiveSolvers.ContainsKey(subjectType))
            {
                return ActiveSolvers[subjectType];
            }
            var solver = (Solver)Activator.CreateInstance(GetSolverType(subjectType));
            ActiveSolvers.Add(subjectType, solver);
            return solver;
        }

        private static Type GetSolverType(SubjectType subjectType)
        {
            switch (subjectType)
            {
                case SubjectType.PODSTAWY_ELEKTRONIKI:
                    return typeof(ElectronicBasicsSolver);
                case SubjectType.PODSTAWY_TEORII_SYGNALOW:
                    return typeof(SignalTheoryBasicsSolver);
                case SubjectType.MECHANIKA:

                case SubjectType.PODSTAWY_AUTOMATYKI:

                case SubjectType.METODY_NUMERYCZNE:

                case SubjectType.ANGIELSKI:

                case SubjectType.ELEMENTY_OPTYKI_I_AKUSTYKI:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(subjectType), subjectType, null);
            }
        }

        public static Dictionary<SubjectType, Solver> ActiveSolvers { get; } = new Dictionary<SubjectType, Solver>();
    }
}
