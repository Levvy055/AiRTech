﻿using System;
using System.Collections.Generic;
using System.IO;
using AiRTech.Core.Misc;
using AiRTech.Core.Net;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Formul
{
    public class Formula : Item, IComparable
    {
        #region Equality
        public override bool Equals(object o)
        {
            var y = o as Formula;
            return !ReferenceEquals(y, null) && Equals(y);
        }

        public bool Equals(Formula y)
        {
            if (ReferenceEquals(this, y)) { return true; }
            if (this.GetType() != y.GetType()) { return false; }
            return string.Equals(this.Title, y.Title)
                && string.Equals(this.EqFile, y.EqFile)
                && Equals(this.InEqs, y.InEqs);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 397) ^ (Title?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (EqFile?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (InEqs?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            var otherFormula = obj as Formula;
            if (otherFormula != null)
                return string.Compare(Title, otherFormula.Title, StringComparison.Ordinal);
            throw new ArgumentException("Object is not a Formula");
        }
        #endregion

        [JsonProperty("Syn")]
        public string[] Synonyms { get; set; }
        [JsonProperty("Eq")]
        public string EqFile { get; set; }
        [JsonProperty("Inner")]
        public InEq[] InEqs { get; set; }

        [JsonIgnore]
        public ImageSource ImageSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(EqFile))
                {
                    return ImageResourceExtension.DefaultEmptyImage;
                }
                var path = Path.Combine(WebCore.FnFmlsDir, WebCore.FnImgDir, EqFile);
                var imageSource = ImageResourceExtension.GetImageFromUri(path).Result;
                return imageSource;
            }
        }
        public static Dictionary<SubjectType, Formula> ActiveFormulas { get; } = new Dictionary<SubjectType, Formula>();
    }
}
