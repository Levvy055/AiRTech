using System.Collections.Generic;
using Xamarin.Forms;

namespace AiRTech.Core.Subjects.Solv.Solvers.Components
{
    public class SvTField : ViewComponent
    {
        public SvTField(string name, IDictionary<string, ViewComponent> uc = null, string placeholder = null, string initValue = "") : base(ViewComponentType.TextField, name)
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

        public string Text
        {
            get
            {
                var tf = Source as Entry;
                return tf.Text;
            }
            set
            {
                var tf = Source as Entry;
                tf.Text=value;
            }
        }
    }
}
