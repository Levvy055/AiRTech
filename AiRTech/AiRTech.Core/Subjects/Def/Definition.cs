﻿using System;
using System.Collections.Generic;
using AiRTech.Core.Subjects.Formul;
using Newtonsoft.Json;

namespace AiRTech.Core.Subjects.Def
{
    public class Definition : Item, IComparable
    {

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
                && Equals(this.SolverNames, y.SolverNames)
                && Equals(FormulaNames, y.FormulaNames);
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
                hashCode = (hashCode * 397) ^ (FormulaNames?.GetHashCode() ?? 0);
                return hashCode;
            }
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

        public InDef[] Inner { get; set; }
        [JsonProperty(PropertyName = "Calcs")]
        public string SolverNames { get; set; }
        [JsonProperty(PropertyName = "Fmls")]
        public string FormulaNames { get; set; }
    }
}
