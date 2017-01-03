using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class DefinitionsPage : ContentPage
    {

        public DefinitionsPage(Subject subject)
        {
            Subject = subject;
            InitializeComponent();
        }

        public Subject Subject { get; set; }
    }
}
