using System.Collections.Generic;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvTxtField : ViewComponent
    {
        public SvTxtField(string name, IDictionary<string, ViewComponent> uc = null, string placeholder = null, string initValue = "") : base(ViewComponentType.TextField, name)
        {
            var tf = new Entry
            {
                Placeholder = placeholder,
                Text = initValue,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 10
            };
            Source = tf;
            uc?.Add(name, this);
        }
    }
}
