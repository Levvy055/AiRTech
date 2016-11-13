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
        public static ICommand SubjectItemClicked
        {
            get
            {
                return new Command(o =>
                {
                    var s = o as Subject;
                    if (s == null) { return; }
                    Debug.WriteLine("Subject Item Selected: " + s.Name);
                    var app = Application.Current as App;
                    if (app == null) { return; }
                    app.ChangePageTo(typeof(SubjectPage), s.Name, true, s);
                });
            }
        }

        public static ICommand DefinitionsTappedCommand
        {
            get
            {
                return new Command(o =>
                {
                    Debug.WriteLine("Definitions tapped");
                }
                );
            }
        }
        public static ICommand FormulasTappedCommand
        {
            get
            {
                return new Command(o =>
                {
                    Debug.WriteLine("Formulas tapped");
                }
                );
            }
        }
        public static ICommand SolverTappedCommand
        {
            get
            {
                return new Command(o =>
                {
                    Debug.WriteLine("Solver tapped");
                }
                );
            }
        }
    }
}
