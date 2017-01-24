using System;
using System.Diagnostics;
using System.Windows.Input;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Core.Commands
{
    public static class SubjectCommands
    {
        public static ICommand SubjectItemClicked(Type type)
        {
            return CreateBaseSwitchPageCommand("Subject Item Selected", type);
        }

        public static ICommand DefinitionsTappedCommand(Type type)
        {
            return CreateBaseSwitchPageCommand("DefinitionView", type, s => s.Base.LoadDefinitions());
        }

        public static ICommand FormulasTappedCommand(Type type)
        {
            return CreateBaseSwitchPageCommand("Formulas", type, s => s.Base.LoadFormulas());
        }

        public static ICommand SolverTappedCommand(Type type)
        {
            return CreateBaseSwitchPageCommand("Solver", type);
        }

        private static ICommand CreateBaseSwitchPageCommand(string txt, Type pageType, Action<Subject> additionalAction = null)
        {
            return new Command(o =>
            {
                var s = o as Subject;
                if (s == null)
                {
                    return;
                }
                Debug.WriteLine(txt + ": " + s.Name);
                CoreManager.NavigateToMain(pageType, s.Name, true, s);
                if (additionalAction == null)
                {
                    return;
                }
                additionalAction.Invoke(s);
            });
        }
    }
}
