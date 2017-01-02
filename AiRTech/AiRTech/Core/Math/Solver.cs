using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Math.Solvers;
using AiRTech.Core.Subjects;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Core.Math
{
    public abstract class Solver
    {

        public abstract Dictionary<string, SolverView> GetTabs();

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
                    break;
                case SubjectType.PODSTAWY_AUTOMATYKI:
                    break;
                case SubjectType.METODY_NUMERYCZNE:
                    break;
                case SubjectType.ANGIELSKI:
                    break;
                case SubjectType.ELEMENTY_OPTYKI_I_AKUSTYKI:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(subjectType), subjectType, null);
            }
            return null;
        }

        public static Dictionary<SubjectType, Solver> ActiveSolvers { get; } = new Dictionary<SubjectType, Solver>();
    }
}
