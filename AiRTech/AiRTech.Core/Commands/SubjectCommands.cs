﻿using System;
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
                    CoreManager.Current.App.NavigateToSubject(s.Name, s);
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
                    CoreManager.Current.App.NavigateToDefinitionList(s);
                    s.Base.LoadDefinitions();
                });
            }
        }

        public static ICommand FormulasTappedCommand
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
                    Debug.WriteLine("Formulas: " + s.Name);
                    s.Base.LoadFormulas();
                    CoreManager.Current.App.NavigateToFormulaList(s);
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
                    CoreManager.Current.App.NavigateToSolverList(s);
                });
            }
        }
    }
}
