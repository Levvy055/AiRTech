using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Core
{
    public abstract  class AiRTechApp:Application
    {
        protected AiRTechApp()
        {

        }

        public abstract void NavigateToMain(Type mPageType, string title, bool inner = true, params object[] args);
        public abstract void NavigateToSubject(Type sPageType, string title, bool inner = true, params object[] args);
        public abstract void NavigateToDefinition(Type dPageType, string title, bool inner = true, params object[] args);
        public abstract void NavigateToFormula(Type fPageType, string title, bool inner = true, params object[] args);
        public abstract void NavigateToSolver(Type sPageType, string title, bool inner = true, params object[] args);
        public abstract void NavigateToModal(ContentPage modal);
        protected abstract void InitSolvers();
        public IDialogManager DialogManager { get; protected set; }
    }
}
