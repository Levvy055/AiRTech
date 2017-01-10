using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            BindingContext = new AboutViewModel(this);
            InitializeComponent();
        }
    }
}
