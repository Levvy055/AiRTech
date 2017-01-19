using System.Windows.Input;
using AiRTech.Core.Commands;

namespace AiRTech.Views.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        public SubjectViewModel(SubjectPage page) : base(page)
        {
            Title = "Przedmiot " + Subject.Name;
        }

        public string DefinitionTxt => "Definicje";
        public string FormulaTxt => "Wzory";
        public string TaskSolverTxt => "Solver";
        public ICommand DefinitionTappedCommand => SubjectCommands.DefinitionsTappedCommand;
        public ICommand FormulasTappedCommand => SubjectCommands.FormulasTappedCommand;
        public ICommand SolverTappedCommand => SubjectCommands.SolverTappedCommand;
    }
}
