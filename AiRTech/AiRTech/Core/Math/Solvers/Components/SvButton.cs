using System;
using System.Collections.Generic;
using AiRTech.Core.Misc;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvButton : ViewComponent
    {
        private readonly Action<object, EventArgs> _action;

        public SvButton(string name, string text = "", Action<object, EventArgs> action = null, string icon = "circle.png", IDictionary<string, ViewComponent> uc = null) : base(ViewComponentType.Button, name)
        {
            _action = action;
            if (string.IsNullOrWhiteSpace(icon))
            {
                icon = "circle.png";
            }
            var iS = ImageResourceExtension.GetImage("AiRTech.Resources." + icon);
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
            Button = new Button { Text = text, TextColor = Color.White };
            if (_action != null)
            {
                Button.Clicked += OnAction;
            }
            var s = new AbsoluteLayout();
            s.Children.Add(i, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            s.Children.Add(Button, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Source = s;
            uc?.Add(name, this);
        }

        public Button Button { get; }

        public bool IsEnabled
        {
            get
            {
                return Button.IsEnabled;
            }
            set
            {
                Button.IsEnabled = value;
            }
        }

        private void OnAction(object sender, EventArgs e)
        {
            _action.Invoke(sender, e);
        }
    }
}
