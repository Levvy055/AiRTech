using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Solv;
using Xamarin.Forms;

namespace AiRTech.Core
{
    public abstract class AiRTechApp : Application
    {
        protected AiRTechApp()
        {

        }

        protected abstract void InitSolvers();
        public abstract void NavigateToMain(Type pageType, string title);
        public abstract void NavigateToPage(Page page, bool removePrevious = false);
        public abstract void NavigateToSubject(Subject subject, string title);
        public abstract void NavigateToDefinition(string title, Subject subject);
        public abstract void NavigateToFormula(string title, Subject subject);
        public abstract void NavigateToSolverList(Subject subject, string title);
        public abstract void NavigateToSolver(Subject subject, SolverView sv);
        public abstract void NavigateToModal(ContentPage modal);

        public IDialogManager DialogManager { get; protected set; }
        public abstract void NavigateToFormulaList(Subject subject, string title);
    }
}
