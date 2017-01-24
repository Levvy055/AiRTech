using System;
using System.Collections.Generic;
using AiRTech.Core.Subjects.Formul;
using AiRTech.Views.SubjectData;
using AiRTech.Views.ViewComponents;
using Newtonsoft.Json;

namespace AiRTech.Core.Subjects.Def
{
    public class Definition : IComparable
    {
        public void LinkDeserializedComponents(SubjectType st)
        {
            if (!string.IsNullOrEmpty(SolverNames))
            {
                var sTabs = ViewHandler.GetSolverFor(st).SolverTabs;
                foreach (var sns in SolverNames.Split('|'))
                {
                    var sn = sns.Trim();
                    if (!sTabs.ContainsKey(sn)) { continue; }
                    if (!Solvers.Contains(sTabs[sn]))
                    {
                        Solvers.Add(sTabs[sn]);
                    }
                }
            }
            if (!string.IsNullOrEmpty(FormulaNames))
            {
                var sTabs = Formula.GetFormulaFor(st).Tabs;
                foreach (var sns in SolverNames.Split('|'))
                {
                    var sn = sns.Trim();
                    if (!sTabs.ContainsKey(sn)) { continue; }
                    if (!Solvers.Contains(sTabs[sn]))
                    {
                        Solvers.Add(sTabs[sn]);
                    }
                }
            }
        }

        #region Equality
        public override bool Equals(object o)
        {
            var y = o as Definition;
            return !ReferenceEquals(y, null) && Equals(y);
        }

        public bool Equals(Definition y)
        {
            if (ReferenceEquals(this, y)) { return true; }
            if (this.GetType() != y.GetType()) { return false; }
            return string.Equals(this.Title, y.Title)
                && string.Equals(this.Desc, y.Desc)
                && Equals(this.SolverNames, y.SolverNames);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 397) ^ (Title?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Desc?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Inner?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SolverNames?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public static int GetHashCode(Definition obj)
        {
            return obj.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            var otherDefinition = obj as Definition;
            if (otherDefinition != null)
                return string.Compare(Title, otherDefinition.Title, StringComparison.Ordinal);
            throw new ArgumentException("Object is not a Definition");
        }
        #endregion

        public string Title { get; set; }
        public string Desc { get; set; }
        public InDef[] Inner { get; set; }
        [JsonProperty(PropertyName = "Calcs")]
        public string SolverNames { get; set; }
        [JsonProperty(PropertyName = "Fmls")]
        public string FormulaNames { get; set; }
        [JsonIgnore]
        public List<SolverView> Solvers { get; } = new List<SolverView>();
        public List<FormulaView> Formulas { get; } = new List<FormulaView>();
    }
}
