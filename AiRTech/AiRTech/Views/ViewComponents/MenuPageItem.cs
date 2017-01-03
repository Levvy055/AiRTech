using System;
using Xamarin.Forms;

namespace AiRTech.Views.ViewComponents
{
    public class MenuPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }
        public ImageSource IconProperty => ImageSource.FromResource(IconSource);
        public Type TargetType { get; set; }
    }
}
