using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvLabel : ViewComponent
    {
        public SvLabel(string text) : base(ViewComponentType.Label)
        {
            var lbl = new Label
            {
                Text = text,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Source = lbl;
        }
    }
}
