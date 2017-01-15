using AiRTech.Core.Subjects;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class DefinitionsPage : ContentPage
    {

        public DefinitionsPage(Subject subject)
        {
            Subject = subject;
            BindingContext = new DefinitionsViewModel(this);
            InitializeComponent();
        }

        public Subject Subject { get; set; }
    }
}
