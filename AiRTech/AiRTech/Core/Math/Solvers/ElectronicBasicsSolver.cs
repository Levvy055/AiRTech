using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Views.ViewComponents;
using Xamarin.Forms;

namespace AiRTech.Core.Math.Solvers
{
    public class ElectronicBasicsSolver : Solver
    {
        public override List<SolverView> GetTabs()
        {
            var list = new List<SolverView>
            {
                new SolverView()
            };
            return list;
        }
    }
}
