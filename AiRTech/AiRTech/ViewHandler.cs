using System;
using System.Collections.Generic;
using System.Linq;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Solv;
using AiRTech.Core.Subjects.Solv.Solvers;
using AiRTech.Views.SubjectData;
using AiRTech.Views.ViewComponents;

namespace AiRTech
{
    public static class ViewHandler
    {
        private static readonly Dictionary<SubjectType, Dictionary<string, DefinitionView>> _defViews = new Dictionary<SubjectType, Dictionary<string, DefinitionView>>();
        private static readonly Dictionary<SubjectType, Dictionary<string, FormulaView>> _fmlViews = new Dictionary<SubjectType, Dictionary<string, FormulaView>>();
        private static readonly Dictionary<SubjectType, Dictionary<string, SolverView>> _solverViews = new Dictionary<SubjectType, Dictionary<string, SolverView>>();

        #region Add
        public static void Add(DefinitionView defView, SubjectType subjectType)
        {
            if (!DefViews.ContainsKey(subjectType))
            {
                DefViews.Add(subjectType, new Dictionary<string, DefinitionView>());
            }
            var dvd = DefViews[subjectType];
            dvd.Add(defView.Title, defView);
        }

        public static void Add(FormulaView fmlView, SubjectType subjectType)
        {
            if (!FmlViews.ContainsKey(subjectType))
            {
                FmlViews.Add(subjectType, new Dictionary<string, FormulaView>());
            }
            var fvd = FmlViews[subjectType];
            fvd.Add(fmlView.Title, fmlView);
        }

        public static void Add(SolverView solverView, SubjectType subjectType)
        {
            if (!SolverViews.ContainsKey(subjectType))
            {
                SolverViews.Add(subjectType, new Dictionary<string, SolverView>());
            }
            var svd = SolverViews[subjectType];
            svd.Add(solverView.Title, solverView);
        }
        #endregion

        #region Get
        public static DefinitionView GetDefView(SubjectType subjectType, string name)
        {
            if (DefViews.ContainsKey(subjectType))
            {
                var dvd = DefViews[subjectType];
                if (dvd.ContainsKey(name))
                {
                    return dvd[name];
                }
            }
            return null;
        }

        public static FormulaView GetFmlView(SubjectType subjectType, string name)
        {
            if (FmlViews.ContainsKey(subjectType))
            {
                var fvd = FmlViews[subjectType];
                if (fvd.ContainsKey(name))
                {
                    return fvd[name];
                }
            }
            return null;
        }

        public static SolverView GetSolverView(SubjectType subjectType, string name)
        {
            if (SolverViews.ContainsKey(subjectType))
            {
                var svd = SolverViews[subjectType];
                if (svd.ContainsKey(name))
                {
                    return svd[name];
                }
            }
            return null;
        }
        #endregion

        #region GetAll
        public static List<DefinitionView> GetDefViews(SubjectType subjectType)
        {
            if (DefViews.ContainsKey(subjectType))
            {
                var dvd = DefViews[subjectType];
                return dvd.Select(dvp => dvp.Value).ToList();
            }
            return null;
        }

        public static List<FormulaView> GetFmlViews(SubjectType subjectType)
        {
            if (FmlViews.ContainsKey(subjectType))
            {
                var fvd = FmlViews[subjectType];
                return fvd.Select(fvp => fvp.Value).ToList();
            }
            return null;
        }

        public static List<SolverView> GetSolverViews(SubjectType subjectType)
        {
            if (SolverViews.ContainsKey(subjectType))
            {
                var svd = SolverViews[subjectType];
                return svd.Select(svp => svp.Value).ToList();
            }
            return null;
        }
        #endregion

        public static Dictionary<SubjectType, Dictionary<string, DefinitionView>> DefViews => _defViews;
        public static Dictionary<SubjectType, Dictionary<string, FormulaView>> FmlViews => _fmlViews;
        public static Dictionary<SubjectType, Dictionary<string, SolverView>> SolverViews => _solverViews;
    }
}
