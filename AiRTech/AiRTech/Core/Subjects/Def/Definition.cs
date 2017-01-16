using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects.Solv;
using AiRTech.Views.SubjectData;
using AiRTech.Views.ViewComponents;

namespace AiRTech.Core.Subjects.Def
{
    public class Definition
    {
        public Definition()
        {
            Inner=new List<InDef>();
        }

        public string Title { get; set; }
        public string Desc { get; set; }
        public List<InDef> Inner { get; set; }
        public List<SolverView> Solvers { get; }=new List<SolverView>();
    }
}
