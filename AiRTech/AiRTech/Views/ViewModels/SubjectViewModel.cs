using System.Windows.Input;
using AiRTech.Core.Commands;
using AiRTech.Views.Pages;
using AiRTech.Views.SubjectData;

namespace AiRTech.Views.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        private ICommand _defTappedCommand;
        private ICommand _frmlTappedCommand;
        private ICommand _solverTappedCommand;

        public SubjectViewModel(SubjectPage page) : base(page)
        {
            Title = "Przedmiot " + Subject.Name;
        }

        public string DefinitionTxt => "Definicje";
        public string FormulaTxt => "Wzory";
        public string TaskSolverTxt => "Solver";
        public ICommand DefinitionTappedCommand =>
            _defTappedCommand ?? (_defTappedCommand = SubjectCommands.DefinitionsTappedCommand);
        public ICommand FormulasTappedCommand =>
            _frmlTappedCommand ?? (_frmlTappedCommand = SubjectCommands.FormulasTappedCommand);
        public ICommand SolverTappedCommand =>
            _solverTappedCommand ?? (_solverTappedCommand = SubjectCommands.SolverTappedCommand);
    }
}
