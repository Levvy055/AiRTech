using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers
{
    public class SignalTheoryBasicsSolver: Solver
    {
        public override TabbedPage GetView()
        {
            var tp=new TabbedPage()
            {
                Title = "Solver"
            };
            return tp;
        }
    }
}
