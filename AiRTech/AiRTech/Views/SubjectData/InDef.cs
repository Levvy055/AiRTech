using AiRTech.Core.Misc;
using SQLite;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    [Table(("def_components"))]
    public class InDef
    {
        #region Equality
        public bool Equals(InDef other, bool withId)
        {
            return (!withId || ID == other.ID)
                && string.Equals(Image, other.Image)
                && string.Equals(Text, other.Text)
                && Layout == other.Layout
                && DefinitionId == other.DefinitionId;
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
                var hashCode = ID;
                hashCode = (hashCode * 397) ^ (Image?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Text?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (int)Layout;
                hashCode = (hashCode * 397) ^ DefinitionId;
                return hashCode;
            }
        }
        #endregion

        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int ID { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public InDefLayout Layout { get; set; } = InDefLayout.TextOverImage;
        [NotNull]
        public int DefinitionId { get; set; }
        [Ignore]
        public ImageSource ImageSource => ImageResourceExtension.GetImage("AiRTech.Resources.Defs." + Image);
    }
}