using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class ViewComponent
    {
        protected ViewComponent(ViewComponentType compType)
        {
            CompType = compType;
        }

        public ViewComponentType CompType { get; private set; }
        public View Source { get; protected set; }
    }
}
