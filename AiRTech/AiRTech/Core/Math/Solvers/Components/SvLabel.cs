using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvLabel : ViewComponent
    {
        public SvLabel(string text) : base(ViewComponentType.Label)
        {
            var lbl = new Label {Text = text};
            Source = lbl;
        }
    }
}
