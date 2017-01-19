using System;
using AiRTech.Core.Misc;
using Xamarin.Forms;

namespace AiRTech.Views.ViewComponents
{
    public class MenuPageItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public string Detail { get; set; }
        public ImageSource IconProperty => ImageResourceExtension.GetImage(IconSource);
        public Type TargetType { get; set; }
    }
}
