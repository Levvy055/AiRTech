using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvButton:ViewComponent
    {
        public SvButton(string text) : base(ViewComponentType.Button)
        {
            var b=new Button {Text = text};
            Source = b;
        }
    }
}
