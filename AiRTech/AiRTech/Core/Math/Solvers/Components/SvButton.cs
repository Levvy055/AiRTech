using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvButton : ViewComponent
    {
        private readonly Action<object, EventArgs> _action;

        public SvButton(string text = "", Action<object, EventArgs> action = null, string icon = "circle.png") : base(ViewComponentType.Button)
        {
            _action = action;
            var iS = ImageSource.FromResource("AiRTech.Resources." + icon);
            var i = new CircleImage
            {
                BorderColor = Color.White,
                BorderThickness = 3,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Source = iS
            };
            var b = new Button { Text = text, TextColor = Color.White };
            if (_action != null)
            {
                b.Clicked += OnAction;
            }
            var s = new AbsoluteLayout();
            s.Children.Add(i, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            s.Children.Add(b, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Source = s;
        }

        private void OnAction(object sender, EventArgs e)
        {
            _action.Invoke(sender, e);
        }
    }
}
