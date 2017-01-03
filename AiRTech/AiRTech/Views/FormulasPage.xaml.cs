using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class FormulasPage : ContentPage
    {

        public FormulasPage(Subject subject)
        {
            Subject = subject;
            InitializeComponent();
        }

        public Subject Subject { get; set; }
    }
}
