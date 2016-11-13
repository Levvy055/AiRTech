using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AiRTech.Core.Commands;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        public SubjectViewModel(Page page) : base(page)
        {
            Title = "Przedmiot " + ((SubjectPage)page).Subject.Name;
            this.page = page;
        }

        public ICommand DefinitionTappedCommand => SubjectCommands.DefinitionsTappedCommand;
        public ICommand FormulasTappedCommand => SubjectCommands.FormulasTappedCommand;
        public ICommand SolverTappedCommand => SubjectCommands.SolverTappedCommand;

        public string DefinitionTxt => "Definicje";
        public string FormulaTxt => "Wzory";
        public string TaskSolverTxt => "Solver";
    }
}
