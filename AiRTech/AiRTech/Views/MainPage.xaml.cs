using System.Diagnostics;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(this);
            ButtonStart.Clicked +=  (sender, e) =>
            {
                Debug.WriteLine("Btn Start Clicked.");

            };
        }
    }
}
