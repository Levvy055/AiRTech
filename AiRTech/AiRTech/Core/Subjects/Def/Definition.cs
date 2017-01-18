using System.Collections.Generic;
using AiRTech.Core.Subjects.Solv;
using AiRTech.Views.SubjectData;
using AiRTech.Views.ViewComponents;
using Newtonsoft.Json;
using SQLite;

namespace AiRTech.Core.Subjects.Def
{
    [Table("definitions")]
    public class Definition
    {
        public Definition()
        {
        }

        public void LinkDeserializedComponents(SubjectType st)
        {
            SubjectName = st.ToString();
            if (Inner != null && Inner.Length > 0)
            {
                foreach (var inDef in Inner)
                {
                    inDef.DefinitionId = ID;
                }
            }
            if (!string.IsNullOrEmpty(SolverNames))
            {
                var sTabs = Solver.GetSolverFor(st).Tabs;
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

        protected bool Equals(Definition y)
        {
            if (ReferenceEquals(this, y)) { return true; }
            if (this.GetType() != y.GetType()) { return false; }
            return this.ID == y.ID && string.Equals(this.Title, y.Title)
                && string.Equals(this.Desc, y.Desc) && Equals(this.Inner, y.Inner)
                && Equals(this.SolverNames, y.SolverNames);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ID;
                hashCode = (hashCode * 397) ^ (Title?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Desc?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Inner?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SolverNames?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public int GetHashCode(Definition obj)
        {
            unchecked
            {
                var hashCode = obj.ID;
                hashCode = (hashCode * 397) ^ (obj.Title?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Desc?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Inner?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.SolverNames?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
        #endregion

        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int ID { get; set; }
        [NotNull]
        public string Title { get; set; }
        public string SubjectName { get; set; }
        public string Desc { get; set; }
        [Ignore]
        public InDef[] Inner { get; set; }
        [JsonProperty(PropertyName = "Calcs")]
        public string SolverNames { get; set; }
        [Ignore]
        public List<SolverView> Solvers { get; } = new List<SolverView>();
    }
}
