using System.IO;
using AiRTech.Core.Misc;
using AiRTech.Core.Net;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Def
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

        public string Text { get; set; }
        public string Header { get; set; }
        public string List { get; set; }
        public InDefLayout Layout { get; set; } = InDefLayout.TextOverImage;
        public string Image { get; set; }

        [JsonIgnore]
        public ImageSource ImageSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Image))
                {
                    Image = "";
                }
                var path = Path.Combine(WebCore.FnDefDir, WebCore.FnImgDir, Image);
                var imageSource = ImageResourceExtension.GetImageFromUri(path).Result;
                return imageSource;
            }
        }
    }
}