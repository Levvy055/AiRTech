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
        public static double ScreenWidth;
        public static double ScreenHeight;

        protected AiRTechApp()
        {

        }

        protected abstract void InitSolvers();
        public abstract void ClearDefinitions(Subject subjectType);
        public abstract void ClearFormulas(Subject subjectType);
        public abstract void NavigateToModal(ContentPage modal);
        public abstract void NavigateToPage(Page page, bool removePrevious = false);
        public abstract void NavigateToMain(NavPageType pageType, string title);
        public abstract void NavigateBack();
        public abstract void NavigateToSubject(string title, Subject subject);
        public abstract void NavigateToDefinitionList(Subject subject);
        public abstract void NavigateToDefinition(string name, Subject subject);
        public abstract void NavigateToFormulaList(Subject subject);
        public abstract void NavigateToFormula(string name, Subject subject);
        public abstract void NavigateToSolverList(Subject subject);
        public abstract void NavigateToSolver(string solverName, Subject subject);
        public abstract void NavigateToSearchPage(NavPageType callingPage, Subject subject);
        public IDialogManager DialogManager { get; protected set; }
        public CoreManager DataCore { get; protected set; }
        protected Dictionary<NavPageType, Page> MainPages { get; } = new Dictionary<NavPageType, Page>();
        public Dictionary<SubjectType, Dictionary<NavPageType, Page>> SubjectPages { get; } = new Dictionary<SubjectType, Dictionary<NavPageType, Page>>();
    }
}
