﻿using AiRTech.Core.Misc;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public class InDef
    {
        #region Equality
        public bool Equals(InDef other)
        {
            return string.Equals(Image, other.Image)
                && string.Equals(Text, other.Text)
                && Layout == other.Layout;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            return obj.GetType() == this.GetType() && Equals((InDef)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 397) ^ (Image?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Text?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (int)Layout;
                return hashCode;
            }
        }
        #endregion

        public string Image { get; set; }
        public string Text { get; set; }
        public InDefLayout Layout { get; set; } = InDefLayout.TextOverImage;
        [JsonIgnore]
        public ImageSource ImageSource => ImageResourceExtension.GetImage("AiRTech.Resources.Defs." + Image);
    }
}