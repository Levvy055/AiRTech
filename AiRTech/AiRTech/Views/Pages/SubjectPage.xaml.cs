using AiRTech.Core.Subjects;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views.Pages
{
    public partial class SubjectPage : ContentPage
    {

        public SubjectPage(Subject subject)
        {
            Subject = subject;
            BindingContext = new SubjectViewModel(this);
            InitializeComponent();
        }

        public Subject Subject { get; private set; }
    }
}
