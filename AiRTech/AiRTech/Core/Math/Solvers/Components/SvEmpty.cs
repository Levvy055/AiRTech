using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTech.Core.Math.Solvers.Components
{
    public class SvEmpty:ViewComponent
    {
        public SvEmpty() : base(ViewComponentType.Empty)
        {
        }

        public static SvEmpty Elem { get; } = new SvEmpty();
    }
}
