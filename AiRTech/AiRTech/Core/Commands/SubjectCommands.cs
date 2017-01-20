using System;
using System.Diagnostics;
using System.Windows.Input;
using AiRTech.Core.Subjects;
using AiRTech.Views;
using AiRTech.Views.SubjectData;
using Xamarin.Forms;

namespace AiRTech.Core.Commands
{
    public static class SubjectCommands
    {
        public static ICommand SubjectItemClicked => CreateBaseSwitchPageCommand("Subject Item Selected", typeof(SubjectPage));
        public static ICommand DefinitionsTappedCommand => CreateBaseSwitchPageCommand("DefinitionView", typeof(DefinitionsPage));
        public static ICommand FormulasTappedCommand => CreateBaseSwitchPageCommand("Formulas", typeof(FormulasPage));
        public static ICommand SolverTappedCommand => CreateBaseSwitchPageCommand("Solver", typeof(SolverPage));

        private static ICommand CreateBaseSwitchPageCommand(string txt, Type pageType)
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
                app.NavigateTo(pageType, s.Name, true, s);
            });
        }
    }
}
