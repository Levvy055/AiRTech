using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AiRTech.Core.Subjects;
using AiRTech.Views;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Core.Commands
{
    public static class SubjectCommands
    {
        public static ICommand SubjectItemClicked => CreateBaseCommand("Subject Item Selected", typeof(SubjectPage));
        public static ICommand DefinitionsTappedCommand => CreateBaseCommand("SDefinition", typeof(DefinitionsPage));
        public static ICommand FormulasTappedCommand => CreateBaseCommand("Formulas", typeof(FormulasPage));
        public static ICommand SolverTappedCommand => CreateBaseCommand("Solver", typeof(SolverPage));

        private static ICommand CreateBaseCommand(string txt, Type pageType)
        {
            return new Command(o =>
            {
                var s = o as Subject;
                if (s == null)
                {
                    return;
                }
                Debug.WriteLine(txt + ": " + s.Name);
                var app = Application.Current as App;
                if (app == null)
                {
                    return;
                }
                app.ChangePageTo(pageType, s.Name, true, s);
            });
        }
    }
}
