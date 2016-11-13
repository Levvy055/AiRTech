using System.Diagnostics;
using Xamarin.Forms;

namespace AiRTech.Views.ViewComponents
{
    public partial class LoginView : ContentView
    {
        public LoginView()
        {
            InitializeComponent();
            LoginBtn.Clicked += (sender, e) =>
            {
                Debug.WriteLine("Btn Login Clicked.");

            };

        }
    }
}
