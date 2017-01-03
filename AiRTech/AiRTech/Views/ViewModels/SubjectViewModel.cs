using System.Windows.Input;
using AiRTech.Core.Commands;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        public SubjectViewModel(Page page, Subject subject) : base(page)
        {
            Title = "Przedmiot " + subject.Name;
            Subject = subject;
        }

        public Subject Subject { get; private set; }
        public ICommand DefinitionTappedCommand => SubjectCommands.DefinitionsTappedCommand;
        public ICommand FormulasTappedCommand => SubjectCommands.FormulasTappedCommand;
        public ICommand SolverTappedCommand => SubjectCommands.SolverTappedCommand;

        public string DefinitionTxt => "Definicje";
        public string FormulaTxt => "Wzory";
        public string TaskSolverTxt => "Solver";
    }
}
