using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvTxtField: ViewComponent
    {

        public SvTxtField(string placeholder) : base(ViewComponentType.TextField)
        {
            var tf = new Entry { Placeholder = placeholder };
            Source = tf;
        }

        public SvTxtField(string placeholder, string initValue) : base(ViewComponentType.TextField)
        {
            var tf = new Entry {Placeholder = placeholder,Text = initValue};
            Source = tf;
        }
    }
}
