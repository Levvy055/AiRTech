using System;
using System.Diagnostics;
using System.Windows.Input;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Core.Commands
{
    public static class SubjectCommands
    {
        public static ICommand SubjectItemClicked
        {
            get
            {
                return new Command(o =>
                {
                    var s = o as Subject;
                    if (s == null)
                    {
                        return;
                    }
                    Debug.WriteLine("Subject Item Selected: " + s.Name);
                    CoreManager.Current.App.NavigateToSubject(s, s.Name);
                });
            }
        }

        public static ICommand DefinitionsTappedCommand
        {
            get
            {
                return new Command(o =>
                {
                    var s = o as Subject;
                    if (s == null)
                    {
                        return;
                    }
                    Debug.WriteLine("DefinitionView: " + s.Name);
                    CoreManager.Current.App.NavigateToDefinition(s.Name, s);
                    s.Base.LoadDefinitions();
                });
            }
        }

        public static ICommand FormulasTappedCommand
        {
            get
            {
                return new Command(async o =>
                {
                    var s = o as Subject;
                    if (s == null)
                    {
                        return;
                    }
                    Debug.WriteLine("Formulas: " + s.Name);
                    await s.Base.LoadFormulas();
                    CoreManager.Current.App.NavigateToFormulaList(s, s.Name);
                });
            }
        }

        public static ICommand SolverTappedCommand
        {
            get
            {
                return new Command(o =>
                {
                    var s = o as Subject;
                    if (s == null)
                    {
                        return;
                    }
                    Debug.WriteLine("Solver: " + s.Name);
                    CoreManager.Current.App.NavigateToSolverList(s, string.Empty);
                });
            }
        }
    }
}
