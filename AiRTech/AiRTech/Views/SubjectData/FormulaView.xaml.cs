using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Formul;
using Xamarin.Forms;

namespace AiRTech.Views.SubjectData
{
    public partial class FormulaView : ContentView
    {
        private Formula def;
        private Subject subject;

        public FormulaView()
        {
            InitializeComponent();
        }

        public FormulaView(Formula def, Subject subject)
        {
            this.def = def;
            this.subject = subject;
        }
    }
}
