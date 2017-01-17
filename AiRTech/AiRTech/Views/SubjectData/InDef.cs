using AiRTech.Core.Misc;
using SQLite;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public class InDef
    {
        public string Image { get; set; }
        public string Text { get; set; }
        public InDefLayout Layout { get; set; } = InDefLayout.TextOverImage;
        [Ignore]
        public ImageSource ImageSource => ImageResourceExtension.GetImage("AiRTech.Resources.Defs." + Image);
    }
}